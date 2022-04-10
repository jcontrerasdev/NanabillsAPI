using DataAccessLayer.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DomainLayer.Helpers
{
    public class TokenHelper
    {
        public const string Issuer = "https://nanabits.com";
        public const string Audience = "https://nanabits.com";

        //Important note***************
        //The secret is a base64-encoded string, always make sure to use a secure long string so no one can guess it. ever!.
        //a very recommended approach to use is through the HMACSHA256() class, to generate such a secure secret, you can refer to the below function
        // you can run a small test by calling the GenerateSecureSecret() function to generate a random secure secret once, grab it, and use it as the secret above 
        // or you can save it into appsettings.json file and then load it from them, the choice is yours

        public static string GenerateToken(User user, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(secret);

            var ClaimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            });

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = ClaimsIdentity,
                Issuer = Issuer,
                Audience = Audience,
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = signingCredentials,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
