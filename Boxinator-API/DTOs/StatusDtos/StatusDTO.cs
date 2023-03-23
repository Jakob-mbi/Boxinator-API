using System.ComponentModel.DataAnnotations;

namespace Boxinator_API.DTOs.StatusDtos
{
    public class StatusDto
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
