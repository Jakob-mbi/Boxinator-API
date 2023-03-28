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

        public async Task<Country> FindDestinationById(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            return country != null ? country : throw new CountryNotFoundException();
        }
        public async Task<Shipment> CreateNewShipment(Shipment obj)
        {
            var status = await _context.Status.FirstOrDefaultAsync(x => x.Id == 1);
            var country = await _context.Countries.FirstOrDefaultAsync(country => country.Id == obj.DestinationID);
            decimal multiplaier = country.Multiplier;
            obj.StatusList = new List<Status>();
            obj.StatusList.Add(status);
            obj.Price = 200 + (Convert.ToDecimal(obj.Weight)* multiplaier);
            await _context.Shipments.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<IEnumerable<Shipment>> ReadAllPreviousShipmentsForAuthenticatedUser(string userSub)
        {
            var shipments = await _context.Shipments.Include(x => x.StatusList).Include(x => x.Destination).Where(x=>x.UserSub == userSub).ToListAsync();
            return shipments != null ? shipments : throw new ShipmentNotFoundException();
        }
        public async Task<IEnumerable<Shipment>> ReadAllCancelledShipmentsForAuthenticatedUser(string userSub)
        {
            var shipments = await _context.Shipments.Include(x => x.StatusList).Include(x => x.Destination).Where(x => x.StatusList.Any(c => c.Name.ToLower() == "cancelled") && x.UserSub == userSub).ToListAsync();
            return shipments != null ? shipments : throw new ShipmentNotFoundException();
        }

        public async Task<IEnumerable<Shipment>> ReadAllCompletedShipmentsForAuthenticatedUser(string userSub)
        {
            var shipments = await _context.Shipments.Include(x => x.StatusList).Where(x => x.StatusList.Any(c => c.Name.ToLower() == "completed") && x.UserSub == userSub).ToListAsync();
            return shipments != null ? shipments : throw new ShipmentNotFoundException();
        }

        public async Task<IEnumerable<Shipment>> ReadAllShipmentsForAuthenticatedUser(string userSub)
        {
            var shipments = await _context.Shipments.Include(x => x.StatusList).Include(z => z.Destination).Where(x => x.StatusList.Any() && x.UserSub == userSub ).ToListAsync();
            shipments.RemoveAll(x => x.StatusList.Any(x=>x.Name.ToLower() == "cancelled"));
            shipments.RemoveAll(x => x.StatusList.Any(x=>x.Name.ToLower() == "completed"));
            return shipments != null ? shipments : throw new ShipmentNotFoundException();
        }

        public async Task<Shipment> ReadShipmentById(int id, string userSub)
        {
            var shipment = await _context.Shipments.Include(x => x.StatusList).FirstOrDefaultAsync(x => x.Id == id && x.UserSub == userSub);
            return shipment != null ? shipment : throw new ShipmentNotFoundException();
        }
        public async Task<Status> ReadStatusById(int id)
        {
            var status = await _context.Status.FirstOrDefaultAsync(x => x.Id == id);
            return status != null ? status : throw new StatusNotFoundException();
        }

        public async Task<Shipment> UpdateShipment(Shipment shipmentObj)
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
