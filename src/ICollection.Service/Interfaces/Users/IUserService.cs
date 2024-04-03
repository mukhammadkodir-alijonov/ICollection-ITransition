using ICollection.DataAccess.Interfaces.Common;
using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Admins;
using ICollection.Service.Dtos.Users;
using ICollection.Service.ViewModels.AdminViewModels;
using ICollection.Service.ViewModels.CollectionViewModels;
using ICollection.Service.ViewModels.CommentViewModels;
using ICollection.Service.ViewModels.ItemViewModels;
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
        //public Task<PagedList<UserViewModel>> GetAllAysnc(PaginationParams @params);
        //public Task<PagedList<UserViewModel>> SearchAsync(PaginationParams @params, string name);
        //public Task<UserRankViewModel> GetRankAsync(int id);
        public Task<bool> UpdateAsync(int id, UserUpdateDto entity);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> DeleteImageAsync(int id);
        public Task<bool> ImageUpdateAsync(int id, IFormFile file);
        public Task<bool> UpdatePasswordAsync(int id, PasswordUpdateDto dto);
        public Task<PagedList<CollectionViewModel>> GetAllCollectionAsync(PaginationParams @params);
        public Task<PagedList<ItemViewModel>> GetAllItemAsync(int id,PaginationParams @params);
        public Task<PagedList<UserViewModel>> GetAllAsync(PaginationParams @params);
        public Task<CollectionViewModel> GetCollectionById(int id);
        public Task<ItemViewModel> GetItemById(int id);
        public Task<PagedList<CommentViewModel>> GetAllComments(int id, PaginationParams @params);
    }
}
