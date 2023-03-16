using System.ComponentModel.DataAnnotations;

namespace Boxinator_API.Models
{
    public class Roles
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string RoleName { get; set; }
        public ICollection<User> Users { get; set; }


    }
}
