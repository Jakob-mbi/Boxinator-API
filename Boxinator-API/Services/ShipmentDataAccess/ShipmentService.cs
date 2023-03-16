using Boxinator_API.CustomExceptions;
using Boxinator_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Boxinator_API.Services.ShipmentDataAccess
{
    public class ShipmentService : IShipmentService
    {
        private readonly BoxinatorDbContext _context;

        public ShipmentService(BoxinatorDbContext context)
        {
            _context = context;
        }
        public async Task<Shipment> Create(Shipment obj)
        {
            await _context.Shipments.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task Delete(int id)
        {
            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment == null)
            {
                throw new ShipmentNotFoundException();
            }


            _context.Shipments.Remove(shipment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Shipment>> ReadAll()
        {
            return await _context.Shipments.Include(x => x.StatusList).ToListAsync();
        }

        public async Task<IEnumerable<Shipment>> ReadAllShipmentsAllowdForAuthenticatedUser(User user)
        {

            IEnumerable<Shipment> shipments;
            if(user.Role.RoleName == "Admin")
            {
                shipments = await _context.Shipments.Include(x => x.StatusList).Where(x => x.StatusList.Any(c => c.Name != "cancelled" && c.Name != "completed")).ToListAsync();
                return shipments != null? shipments : throw new ShipmentNotFoundException();
            }

            shipments = await _context.Shipments.Include(x => x.StatusList).Where(x => x.UserId == user.Id).ToListAsync();
            return shipments != null ? shipments : throw new ShipmentNotFoundException();

        }

        public Task<IEnumerable<Shipment>> ReadAllShipmentsAllowdForAuthenticatedUserThatIsCancelled(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Shipment>> ReadAllShipmentsAllowdForAuthenticatedUserThatIsCompleted(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<Shipment> ReadById(int id)
        {
            var shipment = await _context.Shipments.Include(x => x.StatusList).FirstOrDefaultAsync(x => x.Id == id);

            if (shipment is null)
            {
                throw new ShipmentNotFoundException();
            }

            return shipment;
        }

        public async Task<Shipment> Update(Shipment obj)
        {
            var shipment = await _context.Shipments.AnyAsync(x => x.Id == obj.Id);
            if (!shipment)
            {
                throw new ShipmentNotFoundException();
            }
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return obj;
        }
    }
}
