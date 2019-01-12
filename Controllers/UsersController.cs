using System.Linq;
using ColaTerminal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        }

        private readonly traperto_kurtContext dbcontext;

        public UsersController(traperto_kurtContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet("[action]/{userId}")]
        public ActionResult GetUser(uint userId)
        {
            var user = dbcontext.User.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new RestUser
            {
                Id = user.Id, 
                UserName = user.UserName, 
                FirstName = user.FirstName, 
                LastName = user.LastName
            });
        }
    }


}