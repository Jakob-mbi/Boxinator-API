using Boxinator_API.DTOs.StatusDtos;
using System.ComponentModel.DataAnnotations;

namespace Boxinator_API.DTOs.ShipmentDtos
{
    public class PutShipmentDTO
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string ReciverName { get; set; }
        [Range(typeof(int), "0", "100")]
        public int Weight { get; set; }
        [MaxLength(100)]
        public string BoxColor { get; set; }
        public int DestinationID { get; set; }
        [MaxLength(100)]
        public string? Email { get; set; }
        public string? UserSub { get; set; }
        [Range(typeof(decimal), "0", "1000")]
        public decimal Price { get; set; }
    }
}
