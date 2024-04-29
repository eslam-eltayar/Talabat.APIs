using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Infrastructure._Identity.DataSeed
{
    public static class ApplicationIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
        {
            if (! userManager.Users.Any())
            {

                var user = new ApplicationUser()
                {
                    DisplayName = "Eslam Eltayar",
                    Email = "tayaruwk@gmail.com",
                    UserName = "eslamtyr",
                    PhoneNumber = "01065357827"
                };

                var user2 = new ApplicationUser()
                {
                    DisplayName = "Ahmed Nasr",
                    Email = "Ahmed.nasr@linkdev.com",
                    UserName = "Ahmed.nasr",
                    PhoneNumber = "01122334455"
                };


                await userManager.CreateAsync(user, "P@ssw0rd"); 
                await userManager.CreateAsync(user2, "Pa$$w0rd"); 
            }
        }
    }
}
