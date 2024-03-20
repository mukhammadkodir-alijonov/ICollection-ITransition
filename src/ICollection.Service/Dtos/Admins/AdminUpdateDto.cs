using ICollection.Domain.Common;
using ICollection.Domain.Entities.Admins;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Dtos.Admins
{
    public class AdminUpdateDto : BaseEntity
    {
        [Required(ErrorMessage = "UserName Required")]
        public string UserName { get; set; } = String.Empty;
        public IFormFile Image { get; set; } = default!;
        public string ImagePath { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
        public DateTime BirthDate { get; set; }
        public static implicit operator Admin(AdminUpdateDto dto)
        {
            return new Admin()
            {
                UserName = dto.UserName,
                ImagePath = dto.ImagePath,
                BirthDate = dto.BirthDate,
                Address = dto.Address
            };
        }
    }
}
