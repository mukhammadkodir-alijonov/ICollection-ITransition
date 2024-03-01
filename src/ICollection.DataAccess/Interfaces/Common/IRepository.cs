using ICollection.Domain.Common;
using System.Linq.Expressions;

namespace ICollection.DataAccess.Interfaces.Common
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task<T?> FindByIdAsync(int id);

        public Task<T?> FirstOrDefault(Expression<Func<T, bool>> expression);

        public T Add(T entity);

        public void Update(int id, T entity);

        public void Delete(int id);
        public void TrackingDeteched(T entity);
    }
}
