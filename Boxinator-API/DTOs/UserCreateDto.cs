using System.ComponentModel.DataAnnotations;

namespace Boxinator_API.DTOs
{
    public class UserCreateDto
    {
        [Required]
        public string Sub { get; set; }
        public string? DateOfBirth { get; set; }
        [MaxLength(100)]
        public string? ZipCode { get; set; }
        [MaxLength(100)]
        public string? Country { get; set; }
        [MaxLength(100)]
        public string? ContactNumber { get; set; }
    }
}
