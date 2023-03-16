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
                throw new ShipmentNotFoundException(id);
            }


            _context.Shipments.Remove(shipment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Shipment>> ReadAll()
        {
            return await _context.Shipments.Include(x => x.Status).ToListAsync();
        }

        public async Task<Shipment> ReadById(int id)
        {
            var shipment = await _context.Shipments.Include(x => x.Status).FirstOrDefaultAsync(x => x.Id == id);

            if (shipment is null)
            {
                throw new ShipmentNotFoundException(id);
            }

            return shipment;
        }

        public async Task<Shipment> Update(Shipment obj)
        {
            var shipment = await _context.Shipments.AnyAsync(x => x.Id == obj.Id);
            if (!shipment)
            {
                throw new ShipmentNotFoundException(obj.Id);
            }
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return obj;
        }
    }
}
