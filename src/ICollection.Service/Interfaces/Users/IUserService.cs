using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Users;
using ICollection.Service.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Interfaces.Users
{
    public interface IUserService
    {
        public Task<PagedList<UserViewModel>> GetAllAysnc(PaginationParams @params);
        public Task<PagedList<UserViewModel>> GetAllUsernameAysnc(PaginationParams @params);
        public Task<UserRankViewModel> GetRankAsync(int id);
        public Task<bool> UpdateAsync(int id, UserUpdateDto entity);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> DeleteImageAsync(int id);
        public Task<bool> ImageUpdateAsync(int id, IFormFile file);
    }
}
