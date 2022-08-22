using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurfsUp.Models;

    public class SurfboardContext : DbContext
    {
        public SurfboardContext (DbContextOptions<SurfboardContext> options)
            : base(options)
        {
        }

        public DbSet<SurfsUp.Models.Surfboard> Surfboard { get; set; } = default!;
    }
