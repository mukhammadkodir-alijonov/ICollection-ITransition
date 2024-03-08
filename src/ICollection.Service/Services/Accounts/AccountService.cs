using ICollection.Service.Dtos.Accounts;
using ICollection.Service.Dtos.Admins;
using ICollection.Service.Dtos.Users;
using ICollection.Service.Interfaces.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Accounts
{
    public class AccountService : IAccountService
    {
        public Task<bool> AdminRegisterAsync(AdminRegisterDto adminRegisterDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByPasswordAsync(UserDeleteDto userDeleteDto)
        {
            throw new NotImplementedException();
        }

        public Task<string> LoginAsync(AccountLoginDto accountLoginDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PasswordUpdateAsync(PasswordUpdateDto passwordUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAsync(AccountRegisterDto registerDto)
        {
            throw new NotImplementedException();
        }

        public Task SendCodeAsync(SendToEmailDto sendToEmail)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyPasswordAsync(UserResetPasswordDto userResetPassword)
        {
            throw new NotImplementedException();
        }
    }
}
