using System;
using System.ComponentModel.DataAnnotations;
using ColaTerminal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ColaTerminal.Services;

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
        private readonly AccountService accountService;

        public RefillController(traperto_kurtContext dbcontext, AccountService accountService)
        {
            this.dbcontext = dbcontext;
            this.accountService = accountService;
        }

        [HttpPost("[action]")]
        public ActionResult Payment([FromBody] RestRefillment input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = accountService.GetCurrentUserForContext(HttpContext);
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
            var refill = new Refill {UserId = user.Id, Price = input.Price};
            dbcontext.Refill.Add(refill);

            foreach (var item in input.Items)
            {
                // Add new log entry in db
                var refillContainment = new RefillContainment
                {
                    RefillId = refill.Id,
                    // ReSharper disable once PossibleInvalidOperationException 
                    // Should be set since it's required in the model
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

                // ReSharper disable once PossibleInvalidOperationException
                // Should be set since it's required in the model
                drink.Quantity += (int) item.Quantity;
                dbcontext.Update(drink);
            }

            dbcontext.SaveChanges();

            return Ok();
        }
    }
}