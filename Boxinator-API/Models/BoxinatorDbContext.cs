using Microsoft.EntityFrameworkCore;

namespace Boxinator_API.Models
{
    public class BoxinatorDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<User> Users { get; set; }

        public BoxinatorDbContext(DbContextOptions<BoxinatorDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData
            (
                new Country { Id = 1, Name = "Sweden", Multiplier = 0 },
                new Country { Id = 2, Name = "Norway", Multiplier = 0 },
                new Country { Id = 3, Name = "Denmark", Multiplier = 0 },
                new Country { Id = 4, Name = "Finland", Multiplier = 0 },
                new Country { Id = 5, Name = "Estonia", Multiplier = 3 },
                new Country { Id = 6, Name = "Latvia", Multiplier = 3 },
                new Country { Id = 7, Name = "Lithuania", Multiplier = 3 },
                new Country { Id = 8, Name = "Germany", Multiplier = 5 },
                new Country { Id = 9, Name = "Poland", Multiplier = 5 },
                new Country { Id = 10, Name = "Netherlands", Multiplier = 6 },
                new Country { Id = 11, Name = "Belgium", Multiplier = 6 },
                new Country { Id = 12, Name = "Luxembourg", Multiplier = 6 },
                new Country { Id = 13, Name = "United Kingdom", Multiplier = 8 },
                new Country { Id = 14, Name = "Ireland", Multiplier = 8 },
                new Country { Id = 15, Name = "France", Multiplier = 7 },
                new Country { Id = 16, Name = "Spain", Multiplier = 9 },
                new Country { Id = 17, Name = "Portugal", Multiplier = 10 },
                new Country { Id = 18, Name = "Czech Republic", Multiplier = 6 },
                new Country { Id = 19, Name = "Austria", Multiplier = 7 },
                new Country { Id = 20, Name = "Switzerland", Multiplier = 7 },
                new Country { Id = 21, Name = "Italy", Multiplier = 9 },
                new Country { Id = 22, Name = "Iceland", Multiplier = 12 }
            );
        }
    }
}
   
