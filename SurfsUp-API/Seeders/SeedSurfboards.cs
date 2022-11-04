using Microsoft.EntityFrameworkCore;
using SurfsUp_Models;

namespace SurfsUp_API.Database
{
    public static class SeedSurfboards
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SurfsUpContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SurfsUpContext>>()))
            {
                // Look for any surfboards.
                if (context.Surfboard.Any())
                {
                    Console.WriteLine("[~] Surfboards have already been Seeded, continuing ...");
                    return;   // DB has been seeded
                }

                context.Surfboard.AddRange(
                    new Surfboard
                    {
                        Title = "The Minilog",
                        Thickness = 2.75F,
                        Width = 21,
                        Length = 6,
                        Volume = 38.8F,
                        Type = "Shortboard",
                        Price = 565
                    },

                    new Surfboard
                    {
                        Title = "The Wide Glider",
                        Thickness = 2.75F,
                        Width = 21,
                        Length = 6,
                        Volume = 38.8F,
                        Type = "Shortboard",
                        Price = 685
                    },

                    new Surfboard
                    {
                        Title = "The Golden Ratio",
                        Thickness = 2.75F,
                        Width = 21,
                        Length = 6,
                        Volume = 38.8F,
                        Type = "Shortboard",
                        Price = 695
                    },

                    new Surfboard
                    {
                        Title = "Mahi Mahi",
                        Thickness = 2.75F,
                        Width = 21,
                        Length = 6,
                        Volume = 38.8F,
                        Type = "Shortboard",
                        Price = 645
                    }
                );
                context.SaveChanges();
                Console.WriteLine("[+] Surfboards have been Seeded!");
            }
        }
    }
}