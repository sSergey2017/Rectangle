using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rectangle.WebApi.JWT;
using Rectangle.WebApi.JWT.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rectangle.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly JWTSettings _options;

        public LoginController(IOptions<JWTSettings> options)
        {
            _options = options.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);
            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }

            return NotFound("user not found");
        }

        private string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Role,user.Role)
            };
            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(UserLogin userLogin)
        {
            var currentUser = UserConstants.Users.FirstOrDefault(x => x.Username.ToLower() ==
                userLogin.Username.ToLower() && x.Password == userLogin.Password);
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }
    }
}
