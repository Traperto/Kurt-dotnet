using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ColaTerminal.Models;
using ColaTerminal.Services;
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
        private readonly AccountService accountService;

        public UsersController(traperto_kurtContext dbcontext, AccountService accountService)
        {
            this.dbcontext = dbcontext;
            this.accountService = accountService;
        }

        [HttpGet("[action]/{userId}")]
        public ActionResult GetUser(uint userId)
        {
            var user = dbcontext.User.Include(x => x.Proceed).ThenInclude(x => x.Drink)
                .FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new RestUser
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Proceeds = user.Proceed.ToList(),
                Drinks = user.Proceed.GroupBy(x => x.Drink)
                    .Select(x => new RestUser.DrinkCounts { Count = x.Count(), Drink = x.Key }).ToList()
            });
        }

        [HttpGet("[action]")]
        public ActionResult GetCurrentUser()
        {

            var user = dbcontext.User.Include(x => x.Proceed).ThenInclude(x => x.Drink)
                .FirstOrDefault(x => x.Id == int.Parse(User.Identity.Name));
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new RestUser
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Proceeds = user.Proceed.ToList(),
                Drinks = user.Proceed.GroupBy(x => x.Drink)
                    .Select(x => new RestUser.DrinkCounts { Count = x.Count(), Drink = x.Key }).ToList()
            });
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        public ActionResult Register([FromBody]LoginInput userParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("you suck");
            }

            var user = new User()
            {
                UserName = userParams.Username,
                Password = TokenController.createHashedPassword(userParams.Password)
            };

            dbcontext.User.Add(user);
            dbcontext.SaveChanges();

            return Ok("user registered");
        }

    }
}