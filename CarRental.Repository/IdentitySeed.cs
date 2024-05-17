using CarRental.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Repository
{
    public static class IdentitySeed
    {
        public static async Task SeedUserAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();


            string[] roleNames = { "Admin", "User" };

            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
            }

            string email = "admin@gmail.com";
            string password = "Test@123";


            if (userManager.FindByEmailAsync(email).Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    FName = "Admin",
                    LName = "Admin",
                    Address = "Portsaid",
                    DrivingLicURl = "",
                    ImageProfileURl="",
                    NationalIdURl="",
                    Email = "admin@gmail.com",
                    DOB = new DateTime(2000, 1, 9),
                    UserName = "Admin",
                    PhoneNumber = "01244662299",
                };

                IdentityResult result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, role: "Admin").Wait();
                }
            }
        }

    }
}
