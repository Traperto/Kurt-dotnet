using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColaTerminal.Models;
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

        [HttpPost("[action]")]
        public bool login([FromBody]LoginInput userParam)
        {
            return false;
            /*
            var user = this.trapertoSsoContext.Users.Where(b => b.EmailAddress == userParam.Username).First();


            HttpContext.Session.SetInt32("userId", (int)user.Id);
            return true;
            */
        }
    }
}