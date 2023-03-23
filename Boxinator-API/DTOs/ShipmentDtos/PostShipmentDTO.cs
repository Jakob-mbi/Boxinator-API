namespace Boxinator_API.DTOs.ShipmentDtos
{
    public class PostShipmentDTO
    {
        public string ReciverName { get; set; }
        public int Weight { get; set; }
        public string BoxColor { get; set; }
        public int DestinationID { get; set; }
        public string? Email { get; set; }
        public string? UserSub { get; set; }
        public decimal Price { get; set; }
    }
}
