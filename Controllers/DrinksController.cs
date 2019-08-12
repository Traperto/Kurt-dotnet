using ColaTerminal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ColaTerminal.Controllers
{
    [Route("api/[controller]")]
    public class DrinksController : Controller
    {
        private readonly traperto_kurtContext dbcontext;

        public DrinksController(traperto_kurtContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet()]
        public ActionResult GetAll()
        {
            var drinks = dbcontext.Drink.ToList();
            return Ok(drinks);
        }
    }
}