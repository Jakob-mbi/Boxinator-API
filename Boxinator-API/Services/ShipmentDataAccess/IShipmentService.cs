using Boxinator_API.CustomExceptions;
using Boxinator_API.Models;

namespace Boxinator_API.Services.ShipmentDataAccess
{
    public interface IShipmentService
    {
        /// <summary>
        /// Find status 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="StatusNotFoundException">Thrown if Shipment is not found</exception>
        /// <returns></returns>
        public Task<Status> ReadStatusById(int id);
    }
}
