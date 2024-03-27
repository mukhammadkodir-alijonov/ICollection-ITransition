using ICollection.Domain.Common;
using ICollection.Domain.Entities.Collections;
using ICollection.Domain.Entities.Users;
using ICollection.Service.Dtos.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Dtos.Users
{
    public class UserViewDto :BaseEntity
    {
        public string UserName { get; set; } = String.Empty;

        public string ImagePath { get; set; } = String.Empty;
        public List<Collection>? Collections { get; set; }

        public DateTime BirthDate { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public FileModelDto? FileModel { get; set; }

        public static implicit operator UserViewDto(User user)
        {
            return new UserViewDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                ImagePath = user.Image!,
                BirthDate = user.BirthDate,
                CreatedAt = user.CreatedAt,    
            };
        }
    }
}
