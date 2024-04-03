using ICollection.Domain.Entities;

namespace ICollection.Service.Interfaces.Common
{
    public interface IAuthService
    {
        public string GenerateToken(Person person, string role);
    }
}
