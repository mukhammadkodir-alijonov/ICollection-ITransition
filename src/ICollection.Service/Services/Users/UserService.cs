using AutoMapper;
using ICollection.DataAccess.Interfaces.Common;
using ICollection.DataAccess.Repositories.Common;
using ICollection.Service.Common.Exceptions;
using ICollection.Service.Common.Helpers;
using ICollection.Service.Common.Security;
using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Admins;
using ICollection.Service.Dtos.Users;
using ICollection.Service.Interfaces.Common;
using ICollection.Service.Interfaces.Users;
using ICollection.Service.ViewModels.CollectionViewModels;
using ICollection.Service.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
/*        public async Task<PagedList<UserViewModel>> SearchAsync(PaginationParams @params, string name)
        {
            var query = _repository.Users.GetAll().Where(x => x.UserName.ToLower().StartsWith(name.ToLower())).OrderByDescending(x => x.CreatedAt).Select(x => _mapper.Map<UserViewModel>(x));
            return await PagedList<UserViewModel>.ToPagedListAsync(query, @params);
        }*/
/*        public async Task<PagedList<UserViewModel>> GetAllAysnc(PaginationParams @params)
        {
            var query = _repository.Users.GetAll().OrderBy(x => x.Id)
                .Select(x => _mapper.Map<UserViewModel>(x));
            return await PagedList<UserViewModel>.ToPagedListAsync(query, @params);
        }*/
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
            var query = _repository.Collections.GetAll().Where(x=>x.UserId ==userid).OrderBy(x => x.Id)
                .Select(x => _mapper.Map<CollectionViewModel>(x));
            return await PagedList<CollectionViewModel>.ToPagedListAsync(query, @params);
        }
    }
}
