using ICollection.Domain.Entities.Admins;
using ICollection.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.ViewModels.AdminViewModels
{
    public class AdminViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = String.Empty;
        public string ImagePath { get; set; } = String.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.Admin;
        public DateTime BirthDate { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = default!;

        public static implicit operator AdminViewModel(Admin model)
        {
            return new AdminViewModel()
            {
                Id = model.Id,
                UserName = model.UserName,
                ImagePath = model.Image,
                BirthDate = model.BirthDate,
                Role = model.AdminRole,
                Email = model.Email,
                Address = model.Address,
                CreatedAt = model.CreatedAt
            };
        }
    }
}
