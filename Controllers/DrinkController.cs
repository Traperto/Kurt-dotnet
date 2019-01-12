using ColaTerminal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ColaTerminal.Controllers
{
    [Route("api/[controller]")]
    public class DrinkController : Controller
    {
        private readonly traperto_kurtContext dbcontext;

        public DrinkController(traperto_kurtContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet("[action]")]
        [Authorize]
        public ActionResult GetAll()
        {
            var drinks = dbcontext.Drink.ToList();
            return Ok(drinks);
        }
    }
}