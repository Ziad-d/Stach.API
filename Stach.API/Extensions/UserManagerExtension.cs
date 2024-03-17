using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stach.Domain.Models.Identity;
using System.Security.Claims;

namespace Stach.API.Extensions
{
    public static class UserManagerExtension
    {
        // Takes 2 parameters (userManager to access Users DBSet, and User of type ClaimsPrincipal to access claims)
        public static async Task<AppUser> FindUserWithAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.Users.Include(U => U.Address).SingleOrDefaultAsync(U => U.Email == email);

            return user;
        }
    }
}
