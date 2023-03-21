using Boxinator_API.CustomExceptions;
using Boxinator_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Boxinator_API.Services.ShipmentDataAccess.Admin
{
    public class ShipmentAdminService : IShipmentAdminService
    {
        private readonly BoxinatorDbContext _context;

        public ShipmentAdminService(BoxinatorDbContext context)
        {
            _context = context;
        }
        public async Task DeleteShipment(int shipmentId)
        {
            var shipment = await _context.Shipments.FindAsync(shipmentId);
            if (shipment == null)
            {
                throw new ShipmentNotFoundException();
            }
            shipment.StatusList.Clear();
            shipment.UserSub = null;
            shipment.User = null;
            _context.Shipments.Remove(shipment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Shipment>> ReadAllCancelledShipmentsForAdmin()
        {
            var shipments = await _context.Shipments.Include(x => x.StatusList).Where(x => x.StatusList.Any(c => c.Name.ToLower() == "cancelled")).ToListAsync();
            return shipments != null ? shipments : throw new ShipmentNotFoundException();
        }

        public async Task<IEnumerable<Shipment>> ReadAllCompletedShipmentsForAdmin()
        {
            var shipments = await _context.Shipments.Include(x => x.StatusList).Where(x => x.StatusList.Any(c => c.Name.ToLower() == "completed")).ToListAsync();
            return shipments != null ? shipments : throw new ShipmentNotFoundException();
        }

        public async Task<IEnumerable<Shipment>> ReadAllCurrentShipmentsForAdmin()
        {
            var shipments = await _context.Shipments.Include(x => x.StatusList).Where(x => x.StatusList.Any(c => c.Name.ToLower() != "completed" && c.Name.ToLower() != "cancelled")).ToListAsync();
            return shipments != null ? shipments : throw new ShipmentNotFoundException();
        }

        public async Task<IEnumerable<Shipment>> ReadShipmentByCustomer(string userSub)
        {
            var shipments = await _context.Shipments.Include(x => x.StatusList).Where(x => x.UserSub == userSub).ToListAsync();
            return shipments != null ? shipments : throw new ShipmentNotFoundException();
        }

       
        public async Task<Shipment> ReadShipmentByIdAdmin(int id)
        {
            var shipment = await _context.Shipments.Include(x => x.StatusList).FirstOrDefaultAsync(x => x.Id == id);

            return shipment != null ? shipment : throw new ShipmentNotFoundException();
        }

        public async Task<Shipment> UpdateShipmentAdmin(Shipment shipmentObj)
        {
            var shipment = await _context.Shipments.AnyAsync(x => x.Id == shipmentObj.Id);
            if (!shipment)
            {
                throw new ShipmentNotFoundException();
            }
            _context.Entry(shipmentObj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return shipmentObj;
        }

    }
}
