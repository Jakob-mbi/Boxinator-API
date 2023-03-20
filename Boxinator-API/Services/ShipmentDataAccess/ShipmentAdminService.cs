using Boxinator_API.Models;

namespace Boxinator_API.Services.ShipmentDataAccess
{
    public class ShipmentAdminService : IShipmentAdminService
    {
        public Task DeleteShipment(int shipmentId)
        {
            throw new NotImplementedException();
        }

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

        public Task<Shipment> ReadShipmentByCustomer(string userSub)
        {
            throw new NotImplementedException();
        }

        public Task<Shipment> ReadShipmentById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Shipment> ReadShipmentByIdAdmin(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Shipment> UpdateShipment(Shipment obj)
        {
            throw new NotImplementedException();
        }

        public Task<Shipment> UpdateShipmentAdmin(int shipmentId)
        {
            throw new NotImplementedException();
        }
    }
}
