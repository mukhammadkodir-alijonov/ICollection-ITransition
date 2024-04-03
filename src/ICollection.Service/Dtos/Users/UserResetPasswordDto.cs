using ICollection.Service.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ICollection.Service.Dtos.Users
{
    public class UserResetPasswordDto
    {
        [Required(ErrorMessage = "Email is required!"), Email]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int Code { get; set; }

        [Required, StrongPassword]
        public string Password { get; set; } = string.Empty;
    }
}
