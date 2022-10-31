using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurfsUp_API.Areas.Identity.Data;
using SurfsUp_API.Models;

namespace SurfsUp_API.Database;

public class SurfsUpContext : IdentityDbContext<SurfsUpUser>
{
    public SurfsUpContext(DbContextOptions<SurfsUpContext> options)
        : base(options)
    {
    }



    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public DbSet<Booking> Booking { get; set; }
    public DbSet<Surfboard>? Surfboard { get; set; }
    public DbSet<Log>? Log { get; set; }
}
