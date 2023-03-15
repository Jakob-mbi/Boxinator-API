using Microsoft.EntityFrameworkCore;

namespace Boxinator_API.Models
{
    public class BoxinatorDbContext : DbContext
    {

        public BoxinatorDbContext(DbContextOptions<BoxinatorDbContext> options)
            : base(options)
        {
        }
    }
}
   
