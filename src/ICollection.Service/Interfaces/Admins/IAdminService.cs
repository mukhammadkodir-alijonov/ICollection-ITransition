using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Admins;
using ICollection.Service.ViewModels.AdminViewModels;
using Microsoft.AspNetCore.Http;

namespace ICollection.Service.Interfaces.Admins
{
    public interface IAdminService
    {
        public Task<bool> DeleteAsync(List<int> ids);
        public Task<bool> BlockAsync(List<int> ids);
        public Task<bool> ActiveAsync(List<int> ids);
        public Task<List<AdminViewModel>> GetAllAsync(string search);
        public Task<PagedList<AdminViewModel>> GetAllAsync(PaginationParams @params);
        //public Task<PagedList<AdminViewModel>> GetByNameAsync(PaginationParams @params, string name);
        public Task<bool> UpdateAsync(int id, AdminUpdateDto adminUpdateDto);
        public Task<bool> UpdateImageAsync(int id, IFormFile from);
        public Task<bool> DeleteImageAsync(int adminId);
        public Task<bool> UpdatePasswordAsync(int id, PasswordUpdateDto dto);
        public Task<bool> CreateAdminAsync(AdminRegisterDto adminCreateDto);
    }
}
