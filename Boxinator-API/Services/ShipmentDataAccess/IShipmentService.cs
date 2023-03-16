using Boxinator_API.CustomExceptions;
using Boxinator_API.Models;

namespace Boxinator_API.Services.ShipmentDataAccess
{
    public interface IShipmentService : ICrudRepository<Shipment, int>
    {
        /// <summary>
        /// Get all Shipments allowd for the authenticated user to view
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ShipmentNotFoundException">Thrown if franchise is not found</exception>
        /// <returns>IEnumerable of Shipment</returns>
        Task<IEnumerable<Shipment>> ReadAllShipmentsAllowdForAuthenticatedUser(int id,Roles role);

    }
}
