using System.ComponentModel.DataAnnotations;

namespace ICollection.Service.Dtos.Users
{
    public class UserDeleteDto
    {
        [Required(ErrorMessage = "Enter your password")]
        public string Password { get; set; } = default!;
    }
}
