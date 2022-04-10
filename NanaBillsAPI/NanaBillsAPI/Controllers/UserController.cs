using DomainLayer.Services.User;
using DTOs.Requests;
using DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace NanaBillsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserService UserService { get; set; }
        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Missing login details");
            }
            var loginResponse = await UserService.Login(loginRequest);

            if (loginResponse == null)
            {
                return BadRequest($"Invalid credentials");
            }

            return Ok(loginResponse);
        }
    }
}