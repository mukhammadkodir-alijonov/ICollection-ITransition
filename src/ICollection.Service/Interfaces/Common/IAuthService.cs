using ICollection.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Interfaces.Common
{
    public interface IAuthService
    {
        public string GenerateToken(Person person, string role);
    }
}
