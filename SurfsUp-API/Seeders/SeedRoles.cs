using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurfsUp_Models;

namespace SurfsUp_API.Database
{
    public static class SeedRoles
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new SurfsUpContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SurfsUpContext>>()))
            {
                var roleManager = new RoleStore<IdentityRole>(context);
                // Look for administrator role.
                if (roleManager.Roles.Any())
                {
                    Console.WriteLine("[~] Roles have already been Seeded, continuing ...");
                    return;   // DB has been seeded
                }

                await roleManager.CreateAsync(
                    new IdentityRole
                    {
                        Name = "Administrator",
                        NormalizedName = "Admin"
                    }
                );
                Console.WriteLine("[+] Roles have been Seeded!");
                return;
            }
        }
    }
}