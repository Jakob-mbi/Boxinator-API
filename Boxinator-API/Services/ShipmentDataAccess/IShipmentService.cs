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

        /// <summary>
        /// Updates a Shipment
        /// </summary>
        /// <param name="shipmentObj"></param>
        /// <exception cref="ShipmentNotFoundException">Thrown if Shipment is not found</exception>
        /// <exception cref="StatusAlredyExist">Thrown if Shipmentalredey have that status </exception>
        /// <returns></returns>
        public Task<Shipment> UpdateShipment(Shipment shipmentObj);

    }
}
