using ICollection.DataAccess.DbContexts;
using ICollection.DataAccess.Interfaces;
using ICollection.DataAccess.Repositories.Common;
using ICollection.Domain.Entities.Users;

namespace ICollection.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
