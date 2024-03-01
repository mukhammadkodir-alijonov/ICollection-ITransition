using ICollection.DataAccess.DbContexts;
using ICollection.DataAccess.Interfaces;
using ICollection.DataAccess.Repositories.Common;
using ICollection.Domain.Entities.Admins;
using Microsoft.EntityFrameworkCore;

namespace ICollection.DataAccess.Repositories
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        public AdminRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public async Task<Admin?> GetByEmailAsync(string email)
            => await _dbContext.Admins.FirstOrDefaultAsync(x => x.Email == email);
    }
}
