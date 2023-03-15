using System.ComponentModel.DataAnnotations;

namespace Boxinator_API.Models
{
    public class Status
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<Shipment> Shipments { get; set; }
    }
}
