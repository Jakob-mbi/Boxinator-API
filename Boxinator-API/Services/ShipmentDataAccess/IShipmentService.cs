using Boxinator_API.CustomExceptions;
using Boxinator_API.Models;

namespace Boxinator_API.Services.ShipmentDataAccess
{
    public interface IShipmentService : ICrudRepository<Shipment,User, int>
    {
        /// <summary>
        /// Get all Shipments allowd for the authenticated user to view
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ShipmentNotFoundException">Thrown if franchise is not found</exception>
        /// <returns>IEnumerable of Shipment</returns>
        public Task<IEnumerable<Shipment>> ReadAllShipmentsAllowdForUser(User user);

        /// <summary>
        /// Get all Shipments allowd for the authenticated user that is Completed
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ShipmentNotFoundException">Thrown if franchise is not found</exception>
        /// <returns>IEnumerable of Shipment</returns>
        
        public Task<IEnumerable<Shipment>> ReadAllShipmentsAllowdForAuthenticatedUserThatIsCompleted(User user);
        /// <summary>
        /// Get all Shipments allowd for the authenticated user that is Cancelled 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ShipmentNotFoundException">Thrown if franchise is not found</exception>
        /// <returns>IEnumerable of Shipment</returns>
        public Task<IEnumerable<Shipment>> ReadAllShipmentsAllowdForAuthenticatedUserThatIsCancelled(User user);


    }
}
