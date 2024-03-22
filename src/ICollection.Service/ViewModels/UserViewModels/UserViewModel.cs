using ICollection.Domain.Entities.Admins;
using ICollection.Domain.Entities.Users;
using ICollection.Domain.Enums;
using ICollection.Service.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.ViewModels.UserViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Role UserRole { get; set; } = Role.User;
        public DateTime BirthDate { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = default!;
        public static implicit operator UserViewModel(User model)
        {
            return new UserViewModel()
            {
                Id = model.Id,
                UserName = model.UserName,
                ImagePath = model.Image,
                Email = model.Email,
                UserRole = model.UserRole,
                BirthDate = model.BirthDate,
                CreatedAt = model.CreatedAt
            };
        }
    }
}
