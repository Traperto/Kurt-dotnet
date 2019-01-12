using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using ColaTerminal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ColaTerminal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class RefillController : Controller
    {
        public class RestDrinkContainment
        {
            [Required] public uint? DrinkId { get; set; }
            [Required] public uint? Quantity { get; set; }
        }

        public class RestRefillment
        {
            [Required] public double? Price { get; set; }
            [Required] public RestDrinkContainment[] Items { get; set; }
        }

        private readonly traperto_kurtContext dbcontext;

        public RefillController(traperto_kurtContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpPost("[action]")]
        public ActionResult Payment([FromBody] RestRefillment input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Get user-id by session
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out var userId))
            {
                return BadRequest();
            }

            // Try to load user by id
            var user = dbcontext.User.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            // Make sure that all drinks exist
            if (input.Items.Any(item => !dbcontext.Drink.Any(d => d.Id == item.DrinkId)))
            {
                return NotFound();
            }

            // user should get the money they payed for the refill.
            user.Balance += input.Price;
            dbcontext.Update(user);

            // Create a refill
            var refill = new Refill {UserId = (uint) userId, Price = input.Price};
            dbcontext.Refill.Add(refill);

            foreach (var item in input.Items)
            {
                // Add new log entry in db
                var refillContainment = new RefillContainment
                {
                    RefillId = refill.Id,
                    DrinkId = (uint) item.DrinkId,
                    Quantity = item.Quantity
                };

                dbcontext.RefillContainment.Add(refillContainment);

                // Update drink states
                var drink = dbcontext.Drink.FirstOrDefault(d => d.Id == item.DrinkId);
                if (drink == null)
                {
                    throw new Exception($"Drink for id {item.DrinkId} went missing");
                }

                drink.Quantity += (int) item.Quantity;
                dbcontext.Update(drink);
            }

            dbcontext.SaveChanges();

            return Ok();
        }
    }
}