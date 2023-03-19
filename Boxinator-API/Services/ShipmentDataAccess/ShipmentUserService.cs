using Boxinator_API.Models;

namespace Boxinator_API.Services.ShipmentDataAccess
{
    public class ShipmentUserService : IShipmentService
    {
        private readonly BoxinatorDbContext _context;

        public ShipmentUserService(BoxinatorDbContext context)
        {
            _context = context;
        }
    }
}
