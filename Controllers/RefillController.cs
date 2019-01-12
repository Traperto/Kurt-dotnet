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
            public double price { get; set; }
            public List<RestDrinkContainment> items { get; set; }
        }

        private readonly traperto_kurtContext dbcontext;
        public RefillController(traperto_kurtContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        /**
         * curl -X POST \
            https://localhost:5001/api/Refill/payment \
            -H 'Content-Type: application/json' \
            -H 'Postman-Token: 41a5bc88-eae0-4a69-b7ce-dc2013480bf6' \
            -H 'cache-control: no-cache' \
            -d '{price:10.45,items: [{drinkId:10, quantity:3}]}'
         */
        [HttpPost("[action]")]
        public bool payment([FromBody] RestRefillment input)
        {
            // TODO authentication

            Console.WriteLine(input.price);
            Console.WriteLine(input.items.ToList().Count);
            Console.WriteLine(input.items[0].drinkId);
            //Console.WriteLine($"{input.price} {input.items}");

            return false;
        }
    }
}