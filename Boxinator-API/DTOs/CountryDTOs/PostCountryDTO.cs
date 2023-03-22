using System.ComponentModel.DataAnnotations;

namespace Boxinator_API.DTOs.CountryDTOs
{
    public class PostCountryDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public int Multiplier { get; set; }
    }
}
