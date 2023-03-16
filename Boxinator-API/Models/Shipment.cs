using System.ComponentModel.DataAnnotations;

namespace Boxinator_API.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string ReciverName { get; set; }
        [Required]
        [Range(typeof(int), "0", "100")]
        public int Weight { get;set; }
        [Required]
        [MaxLength(100)]
        public string BoxColor { get; set; }
        public int DestinationID { get; set; }
        [Required]
        [MaxLength(100)]
        public Country Destination { get; set; }
        [MaxLength(100)]
        public string? Email { get; set; }
       
        [MaxLength(100)]
        public User? User { get; set; }
        [Required]
        public int? UserId { get; set; }
        [Required]
        [Range(typeof(decimal), "0", "1000")]
        public decimal Price { get; set; }
        public ICollection<Status> Status { get; set; }

    }
}
