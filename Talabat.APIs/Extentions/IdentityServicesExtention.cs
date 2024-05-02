using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Talabat.Application;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;
using Talabat.Infrastructure._Identity;

namespace Talabat.APIs.Extentions
{
    public static class IdentityServicesExtention
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services)
        {
            Services.AddScoped<ITokenService, TokenService>();

            Services.AddIdentity<ApplicationUser, IdentityRole>()
                                          .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(); // User Manager / SignIn Manager / RoleManager
            return Services;
        }
    }
}
