using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ColaTerminal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ColaTerminal.Controllers
{
    public class AuthController : Controller
    {
        private readonly traperto_kurtContext _dbcontext;
        private IConfiguration _configuration;

        private static string SECRET_KEY;
        public static SymmetricSecurityKey SIGNING_KEY;

        public AuthController(traperto_kurtContext dbcontext, IConfiguration configuration)
        {
            _dbcontext = dbcontext;
            _configuration = configuration;
            SECRET_KEY = configuration["JWTSecretKey"];
            SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
        }

        public class JwtToken
        {
            public string Token { get; set; }
            public DateTime ExpireDate { get; set; }
            public uint UserId { get; set; }
        }
        public class RestToken
        {
            public uint UserId { get; set; }
            public string UserName { get; set; }
            public DateTime ExpireDate { get; set; }
        }

        private string GenerateToken(RestToken restToken)
        {
            var token = new JwtSecurityToken(
                claims: new Claim[] {
                    new Claim("userId", restToken.UserId.ToString()),
                },
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
                signingCredentials: new SigningCredentials(SIGNING_KEY,
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] LoginInput userParam)
        {
            var user = _dbcontext.User.FirstOrDefault(b => b.UserName == userParam.Username);

            if (user == null)
            {
                return NotFound("User could not be found for username: " + userParam.Username);
            }

            SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(userParam.Password));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            var hashedPassword = builder.ToString();
            if (hashedPassword != user.Password)
            {
                return BadRequest("Incorrect password entered!");
            }

            return Ok(new JwtToken()
            {
                Token = GenerateToken(new RestToken()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    ExpireDate = DateTime.Now.AddDays(1)
                }),
                ExpireDate = DateTime.Now.AddDays(1),
                UserId = user.Id
            });
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult IsLoggedIn()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Ok(false);
            }

            return Ok(true);
        }
    }
}