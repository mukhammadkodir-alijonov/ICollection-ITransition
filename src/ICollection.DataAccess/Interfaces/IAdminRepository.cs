using ICollection.DataAccess.Interfaces.Common;
using ICollection.Domain.Entities.Admins;

namespace ICollection.DataAccess.Interfaces
{
    public interface IAdminRepository : IGenericRepository<Admin>
    {
        public Task<Admin?> GetByEmailAsync(string email);
    }
}
