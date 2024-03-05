using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Admins;
using ICollection.Service.ViewModels.AdminViewModels;
using ICollection.Service.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Interfaces.Admins
{
    public interface IAdminService
    {
        public Task<bool> DeleteAsync(int id);
        public Task<bool> BlockAsync(int id);
        public Task<bool> ActiveAsync(int id);
        public Task<List<AdminViewModel>> GetAllAsync(string search);
        public Task<PagedList<AdminViewModel>> GetAllAsync(PaginationParams @params);
        public Task<PagedList<AdminViewModel>> GetByNameAsync(PaginationParams @params, string name);
        public Task<bool> UpdateAsync(int id, AdminUpdateDto adminUpdateDto);
        public Task<bool> UpdateImageAsync(int id, IFormFile from);
        public Task<bool> DeleteImageAsync(int adminId);
        public Task<bool> UpdatePasswordAsync(int id, PasswordUpdateDto dto);
        public Task<bool> CreateAdminAsync(AdminRegisterDto adminCreateDto);
    }
}
