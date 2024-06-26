﻿using AutoMapper;
using ICollection.DataAccess.Interfaces.Common;
using ICollection.Domain.Entities.Admins;
using ICollection.Domain.Entities.Users;
using ICollection.Domain.Enums;
using ICollection.Service.Common.Exceptions;
using ICollection.Service.Common.Helpers;
using ICollection.Service.Common.Security;
using ICollection.Service.Dtos.Accounts;
using ICollection.Service.Dtos.Admins;
using ICollection.Service.Dtos.Users;
using ICollection.Service.Interfaces.Accounts;
using ICollection.Service.Interfaces.Common;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace ICollection.Service.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IUnitOfWork _repository;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, IAuthService authService, IMapper mapper, IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
            this._repository = unitOfWork;
            this._authService = authService;
            this._mapper = mapper;
        }
        public async Task<bool> AdminRegisterAsync(AdminRegisterDto adminRegisterDto)
        {
            var emailcheck = await _repository.Admins.FirstOrDefault(x => x.Email == adminRegisterDto.Email);
            if (emailcheck is not null)
                throw new StatusCodeException(HttpStatusCode.Conflict, "Email alredy exist");

            var hashresult = PasswordHasher.Hash(adminRegisterDto.Password);
            var admin = _mapper.Map<Admin>(adminRegisterDto);
            admin.AdminRole = Role.Admin;
            admin.PasswordHash = hashresult.Hash;
            admin.Salt = hashresult.Salt;
            admin.CreatedAt = TimeHelper.GetCurrentServerTime();
            admin.LastUpdatedAt = TimeHelper.GetCurrentServerTime();
            _repository.Admins.Add(admin);
            var result = await _repository.SaveChangesAsync();
            return result > 0;
        }
        public async Task<bool> RegisterAsync(AccountRegisterDto registerDto)
        {
            var emailcheck = await _repository.Users.FirstOrDefault(x => x.Email == registerDto.Email);
            if (emailcheck is not null)
                throw new StatusCodeException(HttpStatusCode.Conflict, "Email alredy exist");

            var hasherResult = PasswordHasher.Hash(registerDto.Password);
            var user = _mapper.Map<User>(registerDto);
            user.UserRole = Role.User;
            user.PasswordHash = hasherResult.Hash;
            user.Salt = hasherResult.Salt;
            user.Status = StatusType.Active;
            user.CreatedAt = TimeHelper.GetCurrentServerTime();
            user.LastUpdatedAt = TimeHelper.GetCurrentServerTime();
            _repository.Users.Add(user);
            var databaseResult = await _repository.SaveChangesAsync();
            return databaseResult > 0;
        }
        public async Task<string> LoginAsync(AccountLoginDto accountLoginDto)
        {
            var admin = await _repository.Admins.FirstOrDefault(x => x.Email == accountLoginDto.Email);
            if (admin is null)
            {
                var user = await _repository.Users.FirstOrDefault(x => x.Email == accountLoginDto.Email);
                if (user is null) throw new NotFoundException(nameof(accountLoginDto.Email), "No user with this email is found!");
                if (user.Status != StatusType.Blocked)
                {
                    var hasherResult = PasswordHasher.Verify(accountLoginDto.Password, user.Salt, user.PasswordHash);
                    if (hasherResult)
                    {
                        string token = _authService.GenerateToken(user, "user");
                        return token;
                    }
                    else throw new NotFoundException(nameof(accountLoginDto.Password), "Incorrect password!");
                }
                else throw new NotFoundException(nameof(accountLoginDto.Email), "User is blocked!");
            }
            else
            {
                var hasherResult = PasswordHasher.Verify(accountLoginDto.Password, admin.Salt, admin.PasswordHash);
                if (hasherResult)
                {
                    string token = "";
                    if (admin.Email != null)
                    {
                        token = _authService.GenerateToken(admin, "admin");
                        return token;
                    }
                    token = _authService.GenerateToken(admin, "admin");
                    return token;
                }
                else throw new NotFoundException(nameof(accountLoginDto.Password), "Incorrect password!");
            }
        }
        public async Task<bool> DeleteByPasswordAsync(UserDeleteDto userDeleteDto)
        {
            var user = await _repository.Users.FindByIdAsync(HttpContextHelper.UserId);
            if (user is null) throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

            var res = PasswordHasher.Verify(userDeleteDto.Password, user.Salt, user.PasswordHash);
            if (res == false) throw new StatusCodeException(HttpStatusCode.NotFound, "Password is incorrect!");

            return true;
        }
        public async Task<bool> PasswordUpdateAsync(PasswordUpdateDto passwordUpdateDto)
        {
            var user = await _repository.Users.FindByIdAsync(HttpContextHelper.UserId);
            if (user is not null)
            {
                var result = PasswordHasher.Verify(passwordUpdateDto.OldPassword, user.Salt, user.PasswordHash);
                if (result)
                {
                    if (passwordUpdateDto.NewPassword == passwordUpdateDto.VerifyPassword)
                    {
                        var hash = PasswordHasher.Hash(passwordUpdateDto.VerifyPassword);
                        user.Salt = hash.Salt;
                        user.PasswordHash = hash.Hash;
                        _repository.Users.Update(user.Id, user);
                        var res = await _repository.SaveChangesAsync();
                        return res > 0;
                    }
                    else throw new StatusCodeException(HttpStatusCode.BadRequest, "new password and verify password must be match");
                }
                else throw new StatusCodeException(HttpStatusCode.BadRequest, "Invalid Password");
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");
        }
    }
}
