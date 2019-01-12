using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ColaTerminal.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult login([FromBody]LoginInput userParam)
        {
            Console.WriteLine(userParam.Password);

            var user = dbcontext.User.FirstOrDefault(b => b.UserName == userParam.Username);

            if (user == null)
            {
                return NotFound();
            }

            Console.WriteLine(userParam.Username);
            Console.WriteLine(userParam.Password);

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
                return BadRequest("FALSCHES PASSWORT DU SAU!");
            }
            //HttpContext.Session.SetInt32("userId", (int)user.Id);

            this.storeToSession(user.Id);

            return Ok("DAS WAR DAS RICHTIGE PASSWORT. DU TIER!");
        }

        [HttpPost("[action]")]
        public ActionResult logout()
        {
            HttpContext.SignOutAsync();

            return Ok();
        }

        private async void storeToSession(uint userId)
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