using Boxinator_API.CustomExceptions;
using Boxinator_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Boxinator_API.Services.ShipmentDataAccess.User
{
    public class ShipmentUserService : IShipmentUserService
    {
        private readonly BoxinatorDbContext _context;

        public ShipmentUserService(BoxinatorDbContext context)
        {
            _context = context;
        }

        public async Task<Shipment> CancelShipment(int shipmentId, string userSub)
        {
            var shipment = await _context.Shipments.Where(u => u.UserSub == userSub).FirstOrDefaultAsync(x => x.Id == shipmentId);
            if (shipment == null)
            {
                throw new ShipmentNotFoundException();
            }
            var status = _context.Status.Where(s => s.Name.ToLower() == "canceled").FirstOrDefault();
            shipment.StatusList.Add(status);
            status.ShipmentsList.Add(shipment);
            await _context.SaveChangesAsync();
            return shipment;
        }

        public async Task<Shipment> CreateNewShipment(Shipment obj)
        {
            await _context.Shipments.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<IEnumerable<Shipment>> ReadAllCancelledShipmentsForAuthenticatedUser(string userSub)
        {
            var shipments = await _context.Shipments.Include(x => x.StatusList).Where(x => x.StatusList.Any(c => c.Name.ToLower() == "cancelled") && x.UserSub == userSub).ToListAsync();
            return shipments != null ? shipments : throw new ShipmentNotFoundException();
        }

        public async Task<IEnumerable<Shipment>> ReadAllCompletedShipmentsForAuthenticatedUser(string userSub)
        {
            var shipments = await _context.Shipments.Include(x => x.StatusList).Where(x => x.StatusList.Any(c => c.Name.ToLower() == "completed") && x.UserSub == userSub).ToListAsync();
            return shipments != null ? shipments : throw new ShipmentNotFoundException();
        }

        public async Task<IEnumerable<Shipment>> ReadAllShipmentsForAuthenticatedUser(string userSub)
        {
            var shipments = await _context.Shipments.Include(x => x.StatusList).Where(x => x.UserSub == userSub).ToListAsync();
            return shipments != null ? shipments : throw new ShipmentNotFoundException();
        }

        public async Task<Shipment> ReadShipmentById(int id, string userSub)
        {
            var shipment = await _context.Shipments.Include(x => x.StatusList).FirstOrDefaultAsync(x => x.Id == id && x.UserSub == userSub);
            return shipment != null ? shipment : throw new ShipmentNotFoundException();
        }
    }
}
