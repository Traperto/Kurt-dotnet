using System.ComponentModel.DataAnnotations;
using ColaTerminal.Models;
using ColaTerminal.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly AccountService accountService;

        public BalanceController(traperto_kurtContext dbcontext, AccountService accountService)
        {
            this.dbcontext = dbcontext;
            this.accountService = accountService;
        }

        [HttpPost("[action]")]
        [Authorize]
        public ActionResult Payment([FromBody] BalanceInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = accountService.GetCurrentUserForContext(HttpContext);
            if (user == null || user.Id != input.UserId)
            {
                // User should only be able to alter its own balance
                return Unauthorized();
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