using Microsoft.EntityFrameworkCore;

namespace SurfsUp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SurfboardContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SurfboardContext>>()))
            {
                // Look for any surfboards.
                if (context.Surfboard.Any())
                {
                    return;   // DB has been seeded
                }

                context.Surfboard.AddRange(
                    new Surfboard
                    {
                        Image = 
                        Title = "The Minilog",
                        Thickness = 2.75F,
                        Width = 21,
                        Length = 6,
                        Volume = 38.8F,
                        Type = "Shortboard",
                        Price = 565,
                        Equipment = null
                    },

                    new Surfboard
                    {
                        Title = "The Wide Glider",
                        Thickness = 2.75F,
                        Width = 21,
                        Length = 6,
                        Volume = 38.8F,
                        Type = "Shortboard",
                        Price = 685,
                        Equipment = null
                    },

                    new Surfboard
                    {
                        Title = "The Golden Ratio",
                        Thickness = 2.75F,
                        Width = 21,
                        Length = 6,
                        Volume = 38.8F,
                        Type = "Shortboard",
                        Price = 695,
                        Equipment = null
                    },

                    new Surfboard
                    {
                        Title = "Mahi Mahi",
                        Thickness = 2.75F,
                        Width = 21,
                        Length = 6,
                        Volume = 38.8F,
                        Type = "Shortboard",
                        Price = 645,
                        Equipment = null
                    }
                );
                context.SaveChanges();
            }
        }
    }
}