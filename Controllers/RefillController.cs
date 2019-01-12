using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColaTerminal.Models;
using Microsoft.AspNetCore.Mvc;

namespace ColaTerminal.Controllers
{
    [Route("api/[controller]")]
    public class RefillController : Controller
    {
        public class RestDrinkContainment
        {
            public uint drinkId { get; set; }
            public uint quantity { get; set; }
        }
        public class RestRefillment
        {
            public float price { get; set; }
            public List<RestDrinkContainment> items { get; set; }
        }

        private readonly traperto_kurtContext dbcontext;
        public RefillController(traperto_kurtContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpPost("[action]")]
        public bool payment([FromForm] RefillController input)
        {
            // TODO authentication

            Console.WriteLine(input);

            return false;
        }
    }
}