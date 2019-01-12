using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColaTerminal.Models;
using Microsoft.AspNetCore.Mvc;

namespace ColaTerminal.Controllers
{
    [Route("api/[controller]")]
    public class BalanceController : Controller
    {
        public class BalanceInput
        {
            public uint amount { get; set; }
        }

        private readonly traperto_kurtContext dbcontext;
        public BalanceController(traperto_kurtContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpPost("[action]")]
        public bool payment([FromForm] BalanceInput input)
        {
            // TODO authentication

            return false;
        }
    }
}