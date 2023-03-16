namespace Boxinator_API.CustomExceptions
{
    public class ShipmentNotFoundException : Exception
    {
        public ShipmentNotFoundException(int id) : base($"Franchise with id {id} was not found")
        {

        }
    }
}
