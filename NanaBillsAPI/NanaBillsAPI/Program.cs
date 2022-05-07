using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Expense;
using DataAccessLayer.Repositories.Income;
using DomainLayer.Helpers;
using DomainLayer.Services.Expenses;
using DomainLayer.Services.Income;
using DomainLayer.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NanaBillsAPI.Profiles;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithHeaders("*");
                          builder.WithOrigins("*");
                          builder.WithMethods("*");
                      });
});
// Configuration.
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
// Service JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = TokenHelper.Issuer,
                            ValidAudience = TokenHelper.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["SecretToken"]))
                        };

                    });
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DataAccessLayer.Models.NanaBillsContext>(options => options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=WalletSur;Trusted_Connection=True;"));

// Automapper.
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
// Services.
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IExpensesService, ExpensesService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddTransient<IGenericRepository<Expense, long>, ExpenseRepository>();
builder.Services.AddTransient<IGenericRepository<Income, long>, IncomeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
