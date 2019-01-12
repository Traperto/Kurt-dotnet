using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColaTerminal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ColaTerminal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        public class RestUser
        {
            public uint Id { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public List<Proceed> Proceeds { get; set; }
            public List<DrinkCounts> Drinks { get; set; }

            public class DrinkCounts
            {
                public Drink Drink { get; set; }
                public int Count { get; set; }
            }
        }

        private readonly traperto_kurtContext dbcontext;

        public UsersController(traperto_kurtContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet("[action]/{userId}")]
        public ActionResult GetUserData(uint userId)
        {
            // TODO authentification


            User user = dbcontext.User.Include(x => x.Proceed).ThenInclude(x => x.Drink).FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            RestUser newUser = new RestUser
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Proceeds = user.Proceed.ToList(),
                Drinks = user.Proceed.GroupBy(x => x.Drink).Select(x => new RestUser.DrinkCounts { Count = x.Count(), Drink = x.Key }).ToList()
            };
            return Ok(newUser);

        }
    }


}