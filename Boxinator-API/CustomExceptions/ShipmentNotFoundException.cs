namespace Boxinator_API.CustomExceptions
{
    public class ShipmentNotFoundException : Exception
    {
        public ShipmentNotFoundException() : base("No Shipments was not found")
        {

        }
    }
}
