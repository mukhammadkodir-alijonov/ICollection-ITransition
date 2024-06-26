﻿using ICollection.Service.Dtos.Accounts;
using ICollection.Service.Dtos.Admins;
using ICollection.Service.Dtos.Users;

namespace ICollection.Service.Interfaces.Accounts
{
    public interface IAccountService
    {
        public Task<bool> AdminRegisterAsync(AdminRegisterDto adminRegisterDto);
        public Task<bool> RegisterAsync(AccountRegisterDto registerDto);
        public Task<string> LoginAsync(AccountLoginDto accountLoginDto);
        public Task<bool> PasswordUpdateAsync(PasswordUpdateDto passwordUpdateDto);
        public Task<bool> DeleteByPasswordAsync(UserDeleteDto userDeleteDto);
        //public Task SendCodeAsync(SendToEmailDto sendToEmail);
        //public Task<bool> VerifyPasswordAsync(UserResetPasswordDto userResetPassword);
    }
}
