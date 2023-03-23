using Boxinator_API.CustomExceptions;
using Boxinator_API.Models;

namespace Boxinator_API.Services.ShipmentDataAccess.User
{
    public interface IShipmentUserService : IShipmentService
    {
        /// <summary>
        /// Get all Shipments allowd for the authenticated user to view
        /// </summary>
        /// <param name="userSub"></param>
        /// <exception cref="ShipmentNotFoundException">Thrown if franchise is not found</exception>
        /// <returns>IEnumerable of Shipment</returns>
        public Task<IEnumerable<Shipment>> ReadAllCancelledShipmentsForAuthenticatedUser(string userSub);

        /// <summary>
        /// Get all Shipments allowd for the authenticated user that is Completed
        /// </summary>
        /// <param name="userSub"></param>
        /// <exception cref="ShipmentNotFoundException">Thrown if franchise is not found</exception>
        /// <returns>IEnumerable of Shipment</returns>

        public Task<IEnumerable<Shipment>> ReadAllShipmentsForAuthenticatedUser(string userSub);
        /// <summary>
        /// Get all Shipments allowd for the authenticated user that is Cancelled 
        /// </summary>
        /// <param name="userSub"></param>
        /// <exception cref="ShipmentNotFoundException">Thrown if franchise is not found</exception>
        /// <returns>IEnumerable of Shipment</returns>
        public Task<IEnumerable<Shipment>> ReadAllCompletedShipmentsForAuthenticatedUser(string userSub);

        /// <summary>
        /// Post a Shipment to the database
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Task<Shipment> CreateNewShipment(Shipment obj);

        /// <summary>
        /// Get shipment by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userSub"></param>
        /// <exception cref="ShipmentNotFoundException">Thrown if Shipment is not found</exception>
        /// <returns>Shipment</returns>
        public Task<Shipment> ReadShipmentById(int id, string userSub);

        /// <summary>
        /// Cancel a Shipment
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <param name="userSub"></param>
        /// <exception cref="ShipmentNotFoundException">Thrown if Shipment is not found</exception>
        /// <returns></returns>
        public Task<Shipment> CancelShipment(int shipmentId, string userSub);

    }
}
