using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SurfsUpBlazor.Server.Models;
using SurfsUpBlazor.Shared;

namespace SurfsUpBlazor.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Auto populates our Surfboards property on User
            builder.Entity<ApplicationUser>().Navigation(e => e.Surfboards).AutoInclude();
            // Auto populates our Rentals property on User
            builder.Entity<ApplicationUser>().Navigation(e => e.Rentals).AutoInclude();
            // Auto populates our Messages property on User
            builder.Entity<ApplicationUser>().Navigation(e => e.Messages).AutoInclude();
            // Auto populates our Rentals property on Surfboard
            builder.Entity<Surfboard>().Navigation(e => e.Rentals).AutoInclude();
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new SurfboardConfiguration());
        }

        public DbSet<Surfboard> Surfboards => Set<Surfboard>();
        public DbSet<Rental> Rentals => Set<Rental>();
        public DbSet<Message> Messages => Set<Message>();
    }
}