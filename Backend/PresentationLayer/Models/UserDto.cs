using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class UserDto
    {
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int TokensAvailable { get; set; }
    }
}
