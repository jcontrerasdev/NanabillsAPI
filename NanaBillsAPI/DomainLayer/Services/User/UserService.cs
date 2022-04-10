using DataAccessLayer.Models;
using DomainLayer.Helpers;
using DTOs.Requests;
using DTOs.Responses;
using Microsoft.Extensions.Configuration;

namespace DomainLayer.Services.User
{
    public class UserService : IUserService
    {
        private readonly NanaBillsContext _context;
        private readonly IConfiguration _configuration;
        public UserService(NanaBillsContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var name = _configuration["SecretToken"];
            var customer = _context.Users.SingleOrDefault(customer => customer.Username == loginRequest.Username);
            if (customer == null) return null;

            var passwordHash = HashingHelper.EncodeMD5(loginRequest.Password);
            if (passwordHash != customer.Password) return null;

            var token = await Task.Run(() => TokenHelper.GenerateToken(customer, _configuration["SecretToken"]));

            return new LoginResponse { UserName = customer.Username, Token = token };
        }
    }
}
