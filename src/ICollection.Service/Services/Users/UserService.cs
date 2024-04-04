using AutoMapper;
using ICollection.DataAccess.Interfaces.Common;
using ICollection.Service.Common.Exceptions;
using ICollection.Service.Common.Helpers;
using ICollection.Service.Common.Security;
using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Admins;
using ICollection.Service.Dtos.Users;
using ICollection.Service.Interfaces.Common;
using ICollection.Service.Interfaces.Users;
using ICollection.Service.ViewModels.CollectionViewModels;
using ICollection.Service.ViewModels.CommentViewModels;
using ICollection.Service.ViewModels.ItemViewModels;
using ICollection.Service.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace ICollection.Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _repository;
        private readonly IAuthService _authService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public UserService(IUnitOfWork unitOfWork, IAuthService authService, IImageService imageService, IMapper mapper, IIdentityService identityService)
        {
            this._repository = unitOfWork;
            this._authService = authService;
            this._imageService = imageService;
            this._mapper = mapper;
            this._identityService = identityService;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var temp = await _repository.Users.FindByIdAsync(id);
            if (temp is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");
            _repository.Users.Delete(id);
            var result = await _repository.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteImageAsync(int id)
        {
            var student = await _repository.Users.FindByIdAsync(id);
            await _imageService.DeleteImageAsync(student!.Image);
            student.Image = "";
            _repository.Users.Update(id, student);
            var result = await _repository.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> ImageUpdateAsync(int id, IFormFile file)
        {
            var user = await _repository.Users.FindByIdAsync(id);
            if (user == null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "user is not found");
            _repository.Users.TrackingDeteched(user);
            if (user.Image != null)
            {
                await _imageService.DeleteImageAsync(user.Image);
            }
            user.Image = await _imageService.SaveImageAsync(file);
            _repository.Users.Update(id, user);
            int res = await _repository.SaveChangesAsync();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(int id, UserUpdateDto userUpdateDto)
        {
            var student = await _repository.Users.FindByIdAsync(id);
            if (student is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Student is not found");
            else
            {
                _repository.Users.TrackingDeteched(student);
                if (userUpdateDto != null)
                {
                    student.UserName = String.IsNullOrEmpty(userUpdateDto.UserName) ? student.UserName : userUpdateDto.UserName;
                    student.BirthDate = userUpdateDto.BirthDate;
                    student.Image = String.IsNullOrEmpty(userUpdateDto.ImagePath) ? student.Image : userUpdateDto.ImagePath;
                    if (userUpdateDto.Image != null)
                    {
                        student.Image = await _imageService.SaveImageAsync(userUpdateDto.Image);
                    }
                }
                student.LastUpdatedAt = TimeHelper.GetCurrentServerTime();
                _repository.Users.Update(id, student);

                var res = await _repository.SaveChangesAsync();
                return res > 0;
            }
        }

        public async Task<bool> UpdatePasswordAsync(int id, PasswordUpdateDto dto)
        {
            var admin = await _repository.Admins.FindByIdAsync(id);
            if (admin is null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Admin is not found");
            _repository.Admins.TrackingDeteched(admin);
            var res = PasswordHasher.Verify(dto.OldPassword, admin.Salt, admin.PasswordHash);
            if (res)
            {
                if (dto.NewPassword == dto.VerifyPassword)
                {
                    var hash = PasswordHasher.Hash(dto.NewPassword);
                    admin.PasswordHash = hash.Hash;
                    admin.Salt = hash.Salt;
                    _repository.Admins.Update(id, admin);
                    var result = await _repository.SaveChangesAsync();
                    return result > 0;
                }
                else throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "new password and verify" + " password must be match!");
            }
            else throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "Invalid Password");
        }
        public async Task<PagedList<CollectionViewModel>> GetAllCollectionAsync(PaginationParams @params)
        {
            var userid = _identityService.Id ?? 0;
            var query = (from collection in _repository.Collections.GetAll()
                         .Where(x => x.UserId == userid).OrderBy(x => x.Id)
                         let isLiked = _repository.Likes.GetAll().Any(x => x.UserId == userid)
                         let likeCount = _repository.Likes.GetAll().Where(x => x.CollectionId == collection.Id).Count()
                         orderby likeCount descending
                         select new CollectionViewModel
                         {
                             Id = collection.Id,
                             Name = collection.Name,
                             Description = collection.Description,
                             ImagePath = collection.Image,
                             LikeCount = likeCount,
                             UserId = userid,
                             isLiked = isLiked
                         });
            return await PagedList<CollectionViewModel>.ToPagedListAsync(query, @params);
        }

        public async Task<PagedList<ItemViewModel>> GetAllItemAsync(int id, PaginationParams @params)
        {
            var userid = _identityService.Id ?? 0;
            var query = (from item in _repository.Iitems.GetAll()
                        .Where(x => x.UserId == userid && x.CollectionId == id).OrderBy(x => x.Id)
                         let isLiked = _repository.LikeItem.GetAll().Any(x => x.UserId == userid && x.ItemId == item.Id)
                         let likeCount = _repository.LikeItem.GetAll().Where(x => x.ItemId == item.Id).Count()
                         orderby likeCount descending
                         select new ItemViewModel
                         {
                             Id = item.Id,
                             Name = item.Name,
                             Description = item.Description,
                             ImagePath = item.Image,
                             LikeCount = likeCount,
                             CollectionId = item.CollectionId,
                             UserId = userid,
                             IsLiked = isLiked != null
                         });
            return await PagedList<ItemViewModel>.ToPagedListAsync(query, @params);
        }

        public async Task<PagedList<UserViewModel>> GetAllAsync(PaginationParams @params)
        {
            var query = _repository.Users.GetAll().OrderBy(x => x.Id)
                        .Select(x => _mapper.Map<UserViewModel>(x));
            return await PagedList<UserViewModel>.ToPagedListAsync(query, @params);
        }
        public async Task<CollectionViewModel> GetCollectionById(int id)
        {
            //var userid = _identityService.Id ?? 0;
            var res = await _repository.Collections.FindByIdAsync(id);
            if (res is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Collection not found");
            /*if (res.UserId != userid)
                throw new StatusCodeException(HttpStatusCode.Forbidden, "You are not authorized to access this collection");*/
            var temp = (CollectionViewModel)res;
            return temp;
        }

        public async Task<ItemViewModel> GetItemById(int id)
        {
            var res = await _repository.Iitems.FindByIdAsync(id);
            if (res is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Item not found");
            var temp = _mapper.Map<ItemViewModel>(res);
            return temp;
        }
        public async Task<PagedList<CommentViewModel>> GetAllComments(int id, PaginationParams @params)
        {
            var userid = _identityService.Id ?? 0;
            var query = _repository.Comments.GetAll().Where(x => x.ItemId == id).OrderBy(x => x.Id)
                .Select(x => _mapper.Map<CommentViewModel>(x));
            return await PagedList<CommentViewModel>.ToPagedListAsync(query, @params);
        }
    }
}
