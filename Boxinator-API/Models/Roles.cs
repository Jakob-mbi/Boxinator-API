using System.ComponentModel.DataAnnotations;

namespace Boxinator_API.Models
{
    public class Roles
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Role { get; set; }
        public ICollection<User> UsersList { get; set; }


    }
}
