using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NanaBillsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseSecureController : ControllerBase
    {
        protected Claim GetClaim()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            return claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        }
    }
}
