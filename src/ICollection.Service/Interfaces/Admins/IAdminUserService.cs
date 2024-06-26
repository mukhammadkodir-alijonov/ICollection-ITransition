﻿using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Users;
using ICollection.Service.ViewModels.UserViewModels;

namespace ICollection.Service.Interfaces.Admins
{
    public interface IAdminUserService
    {
        public Task<bool> BlockAsync(List<int> ids);
        public Task<bool> ActiveAsync(List<int> ids);
        public Task<bool> DeleteAsync(List<int> ids);
        //public Task<List<UserViewModel>> GetAllAsync(string search);
        public Task<PagedList<UserViewModel>> GetAllAsync(PaginationParams @params);
        //public Task<PagedList<UserViewModel>> GetByNameAsync(PaginationParams @params, string name);
        public Task<bool> UpdateAsync(int id, UserUpdateDto userUpdateDto);
        //public Task<bool> UpdateImageAsync(int id, IFormFile from);
        public Task<bool> DeleteImageAsync(int adminId);
        //public Task<bool> UpdatePasswordAsync(int id, PasswordUpdateDto dto);
    }
}
