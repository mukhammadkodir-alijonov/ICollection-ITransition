using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Dtos.Accounts
{
    public class AccountRegisterDto : AccountLoginDto
    {
        [Required(ErrorMessage = "Enter a name!")]
        public string UserName { get; set; } = String.Empty;
        public DateTime BirthDate { get; set; }
    }
}
