using Microsoft.AspNetCore.Identity;
using Stach.Domain.Models.Identity;
using Stach.Domain.Services;
using Stach.Repository.Identity;
using Stach.Service;

namespace Stach.API.Extensions
{
    public static class IdentityServicesExtention
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAuthService), typeof(AuthService));

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {

            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            return services;
        }
    }
}
