using System.ComponentModel.DataAnnotations;

namespace ICollection.Service.Dtos.Accounts
{
    public class AccountRegisterDto : AccountLoginDto
    {
        [Required(ErrorMessage = "Enter a name!")]
        public string UserName { get; set; } = String.Empty;
        public DateTime BirthDate { get; set; }
    }
}
