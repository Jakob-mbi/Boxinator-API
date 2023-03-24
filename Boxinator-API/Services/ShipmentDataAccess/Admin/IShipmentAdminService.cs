using Boxinator_API.CustomExceptions;
using Boxinator_API.Models;

namespace Boxinator_API.Services.ShipmentDataAccess.Admin
{
    public interface IShipmentAdminService : IShipmentService
    {
        /// <summary>
        /// Get all Shipments allowd for the admin to view
        /// </summary>
        /// <exception cref="ShipmentNotFoundException">Thrown if Shipment  is not found</exception>
        /// <returns>IEnumerable of Shipment</returns>
        public Task<IEnumerable<Shipment>> ReadAllCurrentShipmentsForAdmin();


        /// <summary>
        /// Get all Shipments allowd for the admin that is Completed
        /// </summary>
        /// <exception cref="ShipmentNotFoundException">Thrown if Shipment  is not found</exception>
        /// <returns>IEnumerable of Shipment</returns>
        public Task<IEnumerable<Shipment>> ReadAllCompletedShipmentsForAdmin();

        /// <summary>
        /// Get all Shipments allowd for the admin that is Cancelled 
        /// </summary>
        /// <exception cref="ShipmentNotFoundException">Thrown if Shipment  is not found</exception>
        /// <returns>IEnumerable of Shipment</returns>
        public Task<IEnumerable<Shipment>> ReadAllCancelledShipmentsForAdmin();

        /// <summary>
        /// Get shipment by id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ShipmentNotFoundException">Thrown if Shipment is not found</exception>
        /// <returns>Shipment</returns>
        public Task<Shipment> ReadShipmentByIdAdmin(int id);


        /// <summary>
        /// Get shipment by Customer
        /// </summary>
        /// <param name="userSub"></param>
        /// <exception cref="ShipmentNotFoundException">Thrown if Shipment is not found</exception>
        /// <returns>Shipment</returns>
        public Task<IEnumerable<Shipment>> ReadShipmentByCustomer(string userSub);


        /// <summary>
        /// Deletes Shipment with id
        /// </summary>
        /// <param name="shipment"></param>
        /// <exception cref="ShipmentNotFoundException">Thrown if Shipment is not found</exception>
        /// <returns></returns>
        public Task DeleteShipment(Shipment shipment);
    }
}
