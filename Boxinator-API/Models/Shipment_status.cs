using System.ComponentModel.DataAnnotations;

namespace Boxinator_API.Models
{
    public class Shipment_status
    {
        public int Id { get; set; }
        [Required]
        public int ShipmentID { get; set; }
        [Required]
        [MaxLength(100)]
        public Shipment Shipment { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        [MaxLength(100)]
        public Status Status { get; set; }
    }
}
