using ICollection.DataAccess.DbContexts;
using ICollection.DataAccess.Interfaces.Common;
using ICollection.Domain.Common;
using System.Linq.Expressions;

namespace ICollection.DataAccess.Repositories.Common
{
    public class GenericRepository<T> : BaseRepository<T>, IGenericRepository<T>
    where T : BaseEntity
    {
        public GenericRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public IQueryable<T> GetAll()
            => _dbSet;
        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
            => _dbSet.Where(predicate);
    }
}
