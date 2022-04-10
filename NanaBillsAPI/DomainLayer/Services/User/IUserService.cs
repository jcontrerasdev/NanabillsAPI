using DTOs.Requests;
using DTOs.Responses;

namespace DomainLayer.Services.User
{
    public interface IUserService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}
