using System.ComponentModel.DataAnnotations;

namespace Boxinator_API.Models
{
    public class User
    {
        [Key]
        public string Sub { get; set; }
        public string? DateOfBirth { get; set; }
        [MaxLength(100)]
        public string? ZipCode { get; set; }
        [MaxLength(100)]
        public string? Country { get; set; }
        [MaxLength(100)]
        public string? ContactNumber { get; set; }
        public int RoleId { get; set; }
        [Required]
        [MaxLength(100)]
        public Roles Role { get; set; }
        
        public ICollection<Shipment>? ShipmentsList { get; set; }
    }
}
