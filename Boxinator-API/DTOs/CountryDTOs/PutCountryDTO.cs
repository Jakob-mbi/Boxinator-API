using System.ComponentModel.DataAnnotations;

namespace Boxinator_API.DTOs.CountryDTOs
{
    public class PutCountryDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public int Multiplier { get; set; }
    }
}
