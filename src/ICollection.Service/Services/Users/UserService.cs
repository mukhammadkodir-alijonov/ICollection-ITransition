using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Users;
using ICollection.Service.Interfaces.Users;
using ICollection.Service.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Users
{
    public class UserService : IUserService
    {
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteImageAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<UserViewModel>> GetAllAysnc(PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<UserViewModel>> GetAllUsernameAysnc(PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<UserRankViewModel> GetRankAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ImageUpdateAsync(int id, IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, UserUpdateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
