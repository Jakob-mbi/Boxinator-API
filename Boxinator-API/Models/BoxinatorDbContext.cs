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
    }
}
   
