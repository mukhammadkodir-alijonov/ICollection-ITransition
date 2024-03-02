using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Dtos.Users
{
    public class UserUpdateDto
    {
        [Required, MaxLength(30), MinLength(3)]
        public string UserName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public IFormFile? Image { get; set; }
    }
}
