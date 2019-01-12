// ReSharper disable UnusedAutoPropertyAccessor.Global InconsistentNaming
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ColaTerminal.Models;
using Microsoft.AspNetCore.Mvc;

namespace ColaTerminal.Controllers
{
    [Route("api/[controller]")]
    public class BuyController : Controller
    {
        public class BuyInput
        {
            [Required]
            public uint DrinkId { get; set; }
            
            [Required]
            public string RfId { get; set; }
        }

        private readonly traperto_kurtContext dbcontext;

        public BuyController(traperto_kurtContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpPost("[action]")]
        public ActionResult Buy([FromBody]BuyInput userParam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(userParam);
            }
            
            var user = dbcontext.User.FirstOrDefault(u => u.RfId == userParam.RfId);
            var drink = dbcontext.Drink.FirstOrDefault(d => d.Id == userParam.DrinkId);
            
            if (user == null || drink == null)
            {
                return NotFound();
            }

            var proceed = new Proceed {UserId = user.Id, DrinkId = drink.Id};
            dbcontext.Proceed.Add(proceed);
            dbcontext.SaveChangesAsync();
            
            return Created($"/proceed/{proceed.Id}", proceed);
        }
    }
}