using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public RestUser getUserData(uint userId)
        {
            // TODO authentification

            User user = this.dbcontext.User.Find(userId);

            // TODO user not found

            RestUser newUser = new RestUser();
            newUser.Id = user.Id;
            newUser.UserName = user.UserName;
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            return newUser;

        }
    }


}