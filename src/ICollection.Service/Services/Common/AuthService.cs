using ICollection.Domain.Entities;
using ICollection.Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Common
{
    public class AuthService : IAuthService
    {
        public string GenerateToken(Person person, string role)
        {
            throw new NotImplementedException();
        }
    }
}
