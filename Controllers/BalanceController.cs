using System.ComponentModel.DataAnnotations;
using System.Linq;
using ColaTerminal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColaTerminal.Controllers
{
    [Route("api/[controller]")]
    public class BalanceController : Controller
    {
        public class BalanceInput
        {
            [Required] public double? Amount { get; set; }

            [Required] public uint? UserId { get; set; }
        }

        private readonly traperto_kurtContext dbcontext;

        public BalanceController(traperto_kurtContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpPost("[action]")]
        [Authorize]
        public ActionResult Payment([FromBody] BalanceInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (HttpContext.Session.GetInt32("userId") != input.UserId)
            {
                // User should only be able to alter its own balance
                return Unauthorized();
            }

            var user = dbcontext.User.FirstOrDefault(x => x.Id == input.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var transaction = new BalanceTransaction {UserId = user.Id, Amount = input.Amount};
            dbcontext.BalanceTransaction.Add(transaction);

            user.Balance += transaction.Amount;
            dbcontext.Update(user);

            dbcontext.SaveChanges();

            return Ok();
        }
    }
}