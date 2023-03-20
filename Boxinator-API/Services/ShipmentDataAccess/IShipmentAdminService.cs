using Boxinator_API.CustomExceptions;
using Boxinator_API.Models;

namespace Boxinator_API.Services.ShipmentDataAccess
{
    public interface IShipmentAdminService 
    {
        /// <summary>
        /// Get all Shipments allowd for the admin to view
        /// </summary>
        /// <exception cref="ShipmentNotFoundException">Thrown if franchise is not found</exception>
        /// <returns>IEnumerable of Shipment</returns>
        public Task<IEnumerable<Shipment>> ReadAllCancelledShipmentsForAdmin();

        /// <summary>
        /// Get all Shipments allowd for the admin that is Completed
        /// </summary>
        /// <exception cref="ShipmentNotFoundException">Thrown if franchise is not found</exception>
        /// <returns>IEnumerable of Shipment</returns>

        public Task<IEnumerable<Shipment>> ReadAllShipmentsForAdmin();
        /// <summary>
        /// Get all Shipments allowd for the admin that is Cancelled 
        /// </summary>
        /// <exception cref="ShipmentNotFoundException">Thrown if franchise is not found</exception>
        /// <returns>IEnumerable of Shipment</returns>
        public Task<IEnumerable<Shipment>> ReadAllCompletedShipmentsForAdmin();

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
        public Task<Shipment> ReadShipmentByCustomer(string userSub);

        /// <summary>
        /// Sends a Put 
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="ShipmentNotFoundException">Thrown if Shipment is not found</exception>
        /// <returns></returns>
        public Task<Shipment> UpdateShipment(Shipment obj);

        /// <summary>
        /// Updates a Shipment
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <exception cref="ShipmentNotFoundException">Thrown if Shipment is not found</exception>
        /// <returns></returns>
        public Task<Shipment> UpdateShipmentAdmin(int shipmentId);

        /// <summary>
        /// Deletes Shipment with id
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <exception cref="ShipmentNotFoundException">Thrown if Shipment is not found</exception>
        /// <returns></returns>
        public Task DeleteShipment(int shipmentId);
    }
}
