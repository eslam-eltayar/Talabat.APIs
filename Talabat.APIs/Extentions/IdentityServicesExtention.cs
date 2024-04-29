using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;
using Talabat.Infrastructure._Identity;

namespace Talabat.APIs.Extentions
{
    public static class IdentityServicesExtention
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services)
        {
            Services.AddIdentity<ApplicationUser, IdentityRole>()
                                          .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            Services.AddAuthentication(); // User Manager / SignIn Manager / RoleManager
            return Services;
        }
    }
}
