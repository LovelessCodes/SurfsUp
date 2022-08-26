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
                        Image = "https://www.surfsup.com/images/surfboards/the-minilog.jpg",
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
                        Image = "https://www.surfsup.com/images/surfboards/the-wide-glider.jpg",
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
                        Image = "https://www.surfsup.com/images/surfboards/the-golden-ratio.jpg",
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
                        Image = "https://www.surfsup.com/images/surfboards/mahi-mahi.jpg",
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