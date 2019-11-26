using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ColaTerminal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ColaTerminal.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]
    public class BuyController : Controller
    {
        public class BuyInput
        {
            [Required] public uint DrinkId { get; set; }
            [Required] public string RfId { get; set; }
        }
        public class Order
        {
            public string drink { get; set; }
            public string user { get; set; }
            public double price { get; set; }
        }

        private readonly traperto_kurtContext dbcontext;

        public BuyController(traperto_kurtContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpPost()]
        public ActionResult Buy([FromBody] BuyInput userParam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid body given");
            }

            var drink = dbcontext.Drink.FirstOrDefault(d => d.Id == userParam.DrinkId);
            if (drink == null)
            {
                return NotFound("Getränk nicht gefunden");
            }

            var rfid = dbcontext.Rfid.Include(x => x.User).FirstOrDefault(r => r.rfId == userParam.RfId);
            if (rfid == null)
            {
                return NotFound("Rfid nicht gefunden");
            }

            var user = rfid.User;
            if (user == null)
            {
                return NotFound("User nicht gefunden");
            }

            var proceed = new Proceed { UserId = user.Id, DrinkId = drink.Id, Price = drink.Price };
            if (drink.Quantity == 0)
            {
                return BadRequest("Can not get drink since there should be no more drinks available");
            }

            if (user.Balance >= 0.5)
            {
                return BadRequest("Balance is insufficient");
            }
           
            dbcontext.Proceed.Add(proceed);
            user.Balance -= drink.Price;
            drink.Quantity -= 1;

            dbcontext.Update(user);
            dbcontext.Update(drink);
            dbcontext.SaveChanges();

            return Ok(new Order()
            {
                user = user.UserName,
                drink = drink.Name,
                price = drink.Price
            });
        }
    }
}