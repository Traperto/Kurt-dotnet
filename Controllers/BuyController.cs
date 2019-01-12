using System.ComponentModel.DataAnnotations;
using System.Linq;
using ColaTerminal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColaTerminal.Controllers
{
    [Route("api/[controller]")]
    public class BuyController : Controller
    {
        public class BuyInput
        {
            [Required] public uint DrinkId { get; set; }

            [Required] public string RfId { get; set; }
        }

        private readonly traperto_kurtContext dbcontext;

        public BuyController(traperto_kurtContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpPost("[action]")]
        [Authorize]
        public ActionResult Buy([FromBody] BuyInput userParam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid body given");
            }

            var user = dbcontext.User.FirstOrDefault(u => u.RfId == userParam.RfId);
            var drink = dbcontext.Drink.FirstOrDefault(d => d.Id == userParam.DrinkId);

            if (user == null || drink == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetInt32("userId") != user.Id)
            {
                // User should only be able to buy for themselves
                return Unauthorized();
            }

            var proceed = new Proceed {UserId = user.Id, DrinkId = drink.Id, Price = drink.Price};

            if (drink.Quantity == 0)
            {
                return BadRequest("Can not get drink since there should be no more drinks available");
            }

            dbcontext.Proceed.Add(proceed);

            // Remove drink-price from user-balance and update user. 
            // We do NOT check if user CAN buy it, a negative balance is "okay"
            user.Balance -= drink.Price;
            drink.Quantity -= 1;

            dbcontext.Update(user);
            dbcontext.Update(drink);

            dbcontext.SaveChanges();

            return Ok();
        }
    }
}