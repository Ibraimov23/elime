using ElimeProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ElimeProject
{
    public class AppSeeds
    {
        public static async Task CreateDataAsync(IServiceProvider service)
        {
            UserManager<User> userManager = service.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail = "elime-demo@gmail.com";
            string login = "admin";
            User existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin == null)
            {
                User user = new User
                {
                    FullName = "admin",
                    CompanyName = "null",
                    PhoneNumber = "null",
                    Inn = 000,
                    Ogrn = 000,
                    Email = adminEmail,
                    UserName = login
                };
                await userManager.CreateAsync(user, "ZeS-3M7-4bh-uRU");
            }
        }
    }
}
