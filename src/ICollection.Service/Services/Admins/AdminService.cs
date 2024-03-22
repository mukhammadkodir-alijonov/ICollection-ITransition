using ICollection.DataAccess.Interfaces.Common;
using ICollection.Domain.Entities.Admins;
using ICollection.Domain.Enums;
using ICollection.Service.Common.Exceptions;
using ICollection.Service.Common.Helpers;
using ICollection.Service.Common.Security;
using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Admins;
using ICollection.Service.Interfaces.Admins;
using ICollection.Service.Interfaces.Common;
using ICollection.Service.Interfaces.Files;
using ICollection.Service.ViewModels.AdminViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Admins
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdentityService _identityService;
        private readonly IFileService _fileService;

        public AdminService(IUnitOfWork unitOfWork, IIdentityService identityService, IFileService fileService)
        {
            this._unitOfWork = unitOfWork;
            this._identityService = identityService;
            this._fileService = fileService;
        }
        public async Task<bool> DeleteAsync(List<int> ids)
        {
            foreach (var iteam in ids)
            {
                var temp = await _unitOfWork.Admins.FindByIdAsync(iteam);
                if (temp is not null)
                {
                    _unitOfWork.Admins.Delete(iteam);
                }
            }
            var res = await _unitOfWork.SaveChangesAsync();
            return res > 0;
        }
        public async Task<bool> BlockAsync(List<int> ids)
        {
            foreach (var iteam in ids)
            {
                var temp = await _unitOfWork.Admins.FindByIdAsync(iteam);
                if (temp != null)
                {
                    if (temp.Status != StatusType.Blocked)
                    {
                        temp.Status = StatusType.Blocked;
                        _unitOfWork.Admins.Update(iteam, temp);
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
                var temp = await _unitOfWork.Admins.FindByIdAsync(iteam);
                if (temp != null)
                {
                    if (temp.Status != StatusType.Active)
                    {
                        temp.Status = StatusType.Active;
                        _unitOfWork.Admins.Update(iteam, temp);
                    }
                }
            }
            var res = await _unitOfWork.SaveChangesAsync();
            return res > 0;
        }
        public async Task<bool> DeleteImageAsync(int adminId)
        {
            var admin = await _unitOfWork.Admins.FindByIdAsync(adminId);
            if (admin is null) throw new NotFoundException("Admin", $"{adminId} not found");
            else
            {
                await _fileService.DeleteImageAsync(admin.Image!);
                admin.Image = "";
                _unitOfWork.Admins.Update(adminId, admin);
                var res = await _unitOfWork.SaveChangesAsync();
                return res > 0;
            }
        }
        public async Task<List<AdminViewModel>> GetAllAsync(string search)
        {
            var query = _unitOfWork.Admins.GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.UserName.ToLower().StartsWith(search.ToLower()) || x.Address.ToLower().StartsWith(search.ToLower()));
            }

            var result = await query.OrderByDescending(x => x.CreatedAt).Select(x => (AdminViewModel)x).ToListAsync();
            return result;
        }
        public async Task<PagedList<AdminViewModel>> GetAllAsync(PaginationParams @params)
        {
            var query = from admin in _unitOfWork.Admins.GetAll().OrderByDescending(x => x.CreatedAt).Where(aa => aa.Id == admin.Id)
                        select new AdminViewModel()
                        {
                            Id = admin.Id,
                            UserName = admin.UserName,
                            ImagePath = admin.ImagePath,
                            BirthDate = admin.BirthDate,
                            Address = admin.Address
                        };
            return await PagedList<AdminViewModel>.ToPagedListAsync(query, @params);
        }
        public async Task<bool> UpdateAsync(int id, AdminUpdateDto adminUpdateDto)
        {
            var admin = await _unitOfWork.Admins.FindByIdAsync(id);
            if (admin is null) throw new NotFoundException("Admin", $"{id} not found");
            _unitOfWork.Admins.TrackingDeteched(admin);
            if (adminUpdateDto != null)
            {
                admin.UserName = String.IsNullOrEmpty(adminUpdateDto.UserName) ? admin.UserName : adminUpdateDto.UserName;
                admin.Image = String.IsNullOrEmpty(adminUpdateDto.ImagePath) ? admin.Image : adminUpdateDto.ImagePath;
                admin.BirthDate = admin.BirthDate;
                admin.Address = String.IsNullOrEmpty(adminUpdateDto.Address) ? admin.Address : adminUpdateDto.Address;
                if (adminUpdateDto.Image is not null)
                {
                    admin.Image = await _fileService.UploadImageAsync(adminUpdateDto.Image);
                }
                admin.LastUpdatedAt = TimeHelper.GetCurrentServerTime();
                _unitOfWork.Admins.Update(id, admin);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0;
            }
            else throw new ModelErrorException("", "Not found");
        }
        public async Task<bool> UpdateImageAsync(int id, IFormFile from)
        {
            var admin = await _unitOfWork.Admins.FindByIdAsync(id);
            var updateImage = await _fileService.UploadImageAsync(from);
            var adminUpdatedDto = new AdminUpdateDto()
            {
                ImagePath = updateImage
            };
            var result = await UpdateAsync(id, adminUpdatedDto);
            return result;
        }
        public async Task<bool> UpdatePasswordAsync(int id, PasswordUpdateDto dto)
        {
            var admin = await _unitOfWork.Admins.FindByIdAsync(id);
            if (admin is null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Admin is not found");
            _unitOfWork.Admins.TrackingDeteched(admin);
            var res = PasswordHasher.Verify(dto.OldPassword, admin.Salt, admin.PasswordHash);
            if (res)
            {
                if (dto.NewPassword == dto.VerifyPassword)
                {
                    var hash = PasswordHasher.Hash(dto.NewPassword);
                    admin.PasswordHash = hash.Hash;
                    admin.Salt = hash.Salt;
                    _unitOfWork.Admins.Update(id, admin);
                    var result = await _unitOfWork.SaveChangesAsync();
                    return result > 0;
                }
                else throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "new password and verify" + " password must be match!");
            }
            else throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "Invalid Password");
        }
    }
}
