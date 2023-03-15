using System.ComponentModel.DataAnnotations;

namespace Boxinator_API.Models
{
    public class Country
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public int Multiplier { get; set; }
        public ICollection<Shipment> Shipments { get; set; }
    }
}
