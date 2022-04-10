using DomainLayer.Services.User;
using Microsoft.EntityFrameworkCore;

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
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DataAccessLayer.Models.NanaBillsContext>(options => options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=WalletSur;Trusted_Connection=True;"));
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
