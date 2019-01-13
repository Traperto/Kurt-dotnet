using System.Security.Claims;
using ColaTerminal.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ColaTerminal.Services
{
    public class AccountService
    {
        private readonly traperto_kurtContext dbcontext;

        public AccountService(traperto_kurtContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public User GetCurrentUserForContext(HttpContext context)
        {
            var user = context?.User;
            if (user?.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            // Get user-id by session
            if (!int.TryParse(user.Identity.Name, out var userId))
            {
                return null;
            }

            // Try to load user by id
            return dbcontext.User.FirstOrDefault(u => u.Id == userId);
        }
    }
}