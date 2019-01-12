using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColaTerminal.Models;
using Microsoft.AspNetCore.Mvc;

namespace ColaTerminal.Controllers
{
    [Route("api/[controller]")]
    public class BuyController : Controller
    {
        public class BuyInput
        {
            public uint drinkId { get; set; }
            public string rfId { get; set; }
        }

        private readonly traperto_kurtContext dbcontext;

        public BuyController(traperto_kurtContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpPost("[action]")]
        public bool buy([FromBody]BuyInput userParam)
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