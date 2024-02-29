using Microsoft.AspNetCore.Identity;
using Stach.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stach.Repository.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> _userManager)
        {
            if(_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    DisplayName = "Ziad Saleh",
                    Email = "ziad.saleh@linkdev.com",
                    UserName = "ziad.saleh",
                    PhoneNumber = "01122334455"
                };

                await _userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
