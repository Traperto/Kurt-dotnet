using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ColaTerminal.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ColaTerminal.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly traperto_kurtContext dbcontext;

        public LoginController(traperto_kurtContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public class LoginInput
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public ActionResult Login([FromBody] LoginInput userParam)
        {
            Console.WriteLine(userParam.Password);

            var user = dbcontext.User.FirstOrDefault(b => b.UserName == userParam.Username);

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

            StoreToSession(user.Id);

            return Ok("Successfully logged in");
        }

        [HttpPost("[action]")]
        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();

            return Ok("Successfully logged out");
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public ActionResult IsLoggedIn()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Ok(false);
            }

            return Ok(true);
        }

        private async void StoreToSession(uint userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, authenticationType: CookieAuthenticationDefaults.AuthenticationScheme);

            /* 
                        var authProperties = new AuthenticationProperties
                        {
                        };
            */
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );
        }
    }
}