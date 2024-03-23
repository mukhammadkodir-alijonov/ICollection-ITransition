using AutoMapper;
using ICollection.DataAccess.Interfaces.Common;
using ICollection.Domain.Entities.Admins;
using ICollection.Domain.Enums;
using ICollection.Service.Common.Exceptions;
using ICollection.Service.Common.Helpers;
using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Admins;
using ICollection.Service.Dtos.Users;
using ICollection.Service.Interfaces.Admins;
using ICollection.Service.Interfaces.Common;
using ICollection.Service.Interfaces.Files;
using ICollection.Service.ViewModels.AdminViewModels;
using ICollection.Service.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Admins
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly IFileService _fileService;

        public AdminUserService(IUnitOfWork unitOfWork, IAuthService authService, IMapper mapper, IImageService imageService, IFileService fileService)
        {
            this._unitOfWork = unitOfWork;
            this._authService = authService;
            this._mapper = mapper;
            this._imageService = imageService;
            this._fileService = fileService;
        }
        public async Task<bool> DeleteAsync(List<int> ids)
        {
            foreach (var iteam in ids)
            {
                var temp = await _unitOfWork.Users.FindByIdAsync(iteam);
                if (temp is not null)
                {
                    _unitOfWork.Users.Delete(iteam);
                }
            }
            var res = await _unitOfWork.SaveChangesAsync();
            return res > 0;
        }
        public async Task<bool> BlockAsync(List<int> ids)
        {
            foreach (var iteam in ids)
            {
                var temp = await _unitOfWork.Users.FindByIdAsync(iteam);
                if (temp != null)
                {
                    if (temp.Status != StatusType.Blocked)
                    {
                        temp.Status = StatusType.Blocked;
                        _unitOfWork.Users.Update(iteam, temp);
                    }
                }
            }
            var res = await _unitOfWork.SaveChangesAsync();
            return res > 0;
        }
        public async Task<bool> ActiveAsync(List<int> ids)
        {
            foreach (var iteam in ids)
            {
                var temp = await _unitOfWork.Users.FindByIdAsync(iteam);
                if (temp != null)
                {
                    if (temp.Status != StatusType.Active)
                    {
                        temp.Status = StatusType.Active;
                        _unitOfWork.Users.Update(iteam, temp);
                    }
                }
            }
            var res = await _unitOfWork.SaveChangesAsync();
            return res > 0;
        }

        public async Task<bool> DeleteImageAsync(int userId)
        {
            var admin = await _unitOfWork.Users.FindByIdAsync(userId);
            if (admin is null) throw new NotFoundException("Admin", $"{userId} not found");
            else
            {
                await _fileService.DeleteImageAsync(admin.Image!);
                admin.Image = "";
                _unitOfWork.Users.Update(userId, admin);
                var res = await _unitOfWork.SaveChangesAsync();
                return res > 0;
            }
        }

        public async Task<List<UserViewModel>> GetAllAsync(string search)
        {
            var query = _unitOfWork.Users.GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.UserName.ToLower().StartsWith(search.ToLower()));
            }

            var result = await query.OrderByDescending(x => x.CreatedAt).Select(x => (UserViewModel)x).ToListAsync();
            return result;
        }

        public async Task<PagedList<UserViewModel>> GetAllAsync(PaginationParams @params)
        {
            var query = from admin in _unitOfWork.Admins.GetAll().OrderByDescending(x => x.CreatedAt)
                        select new UserViewModel()
                        {
                            Id = admin.Id,
                            UserName = admin.UserName,
                            ImagePath = admin.Image,
                            BirthDate = admin.BirthDate
                        };
            return await PagedList<UserViewModel>.ToPagedListAsync(query, @params);
        }

        public async Task<bool> UpdateAsync(int id, UserUpdateDto userUpdateDto)
        {
            var user = await _unitOfWork.Users.FindByIdAsync(id);
            if (user is null) throw new NotFoundException("User", $"{id} not found");
            _unitOfWork.Users.TrackingDeteched(user);
            if (userUpdateDto != null)
            {
                user.UserName = String.IsNullOrEmpty(userUpdateDto.UserName) ? user.UserName : userUpdateDto.UserName;
                user.Image = String.IsNullOrEmpty(userUpdateDto.ImagePath) ? user.Image : userUpdateDto.ImagePath;
                user.BirthDate = user.BirthDate;
                if (userUpdateDto.Image is not null)
                {
                    user.Image = await _fileService.UploadImageAsync(userUpdateDto.Image);
                }
                user.LastUpdatedAt = TimeHelper.GetCurrentServerTime();
                _unitOfWork.Users.Update(id, user);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0;
            }
            else throw new ModelErrorException("", "Not found");
        }
    }
}
