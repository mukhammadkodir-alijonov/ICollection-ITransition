using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ICollection.DataAccess.Interfaces.Common
{
    public interface IUnitOfWork
    {
        public IAdminRepository Admins { get; }
        public ICollectionRepository Collections { get; }
        public ICommentRepository Comments { get; }
        public ICustomFieldRepository CustomFields { get; }
        public IitemRepository Iitems { get; }
        public ILikeRepository Likes { get; }
        public ILikeItemRepository LikeItem { get; }
        public ITagRepository Tags { get; }
        public IUserRepository Users { get; }
        public Task<int> SaveChangesAsync();
        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
