using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Admins;
using ICollection.Service.Dtos.Users;
using ICollection.Service.Interfaces.Admins;
using ICollection.Service.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Admins
{
    public class AdminUserService : IAdminUserService
    {
        public Task<bool> ActiveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BlockAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteImageAsync(int adminId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserViewModel>> GetAllAsync(string search)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<UserViewModel>> GetAllAsync(PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<UserViewModel>> GetByNameAsync(PaginationParams @params, string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, UserUpdateDto userUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateImageAsync(int id, IFormFile from)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePasswordAsync(int id, PasswordUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
