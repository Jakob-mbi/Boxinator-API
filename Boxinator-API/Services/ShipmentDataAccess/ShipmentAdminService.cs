using Boxinator_API.Models;

namespace Boxinator_API.Services.ShipmentDataAccess
{
    public class ShipmentAdminService : IShipmentAdminService
    {
        public Task<IEnumerable<Shipment>> ReadAllCancelledShipmentsForAdmin()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Shipment>> ReadAllCompletedShipmentsForAdmin()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Shipment>> ReadAllShipmentsForAdmin()
        {
            throw new NotImplementedException();
        }

        public Task<Shipment> ReadShipmentById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
