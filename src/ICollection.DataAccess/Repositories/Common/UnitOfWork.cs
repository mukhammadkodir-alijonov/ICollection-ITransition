using ICollection.DataAccess.DbContexts;
using ICollection.DataAccess.Interfaces;
using ICollection.DataAccess.Interfaces.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ICollection.DataAccess.Repositories.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public IAdminRepository Admins { get; }

        public ICollectionRepository Collections { get; }

        public ICommentRepository Comments { get; }

        public ICustomFieldRepository CustomFields { get; }

        public IitemRepository Iitems { get; }

        public ILikeRepository Likes { get; }

        public ITagRepository Tags { get; }

        public IUserRepository Users { get; }
        public UnitOfWork(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
            Admins = new AdminRepository(_dbContext);
            Collections = new CollectionRepository(_dbContext);
            Comments = new CommentRepository(_dbContext);
            CustomFields = new CustomFieldRepository(_dbContext);
            Iitems = new itemRepository(_dbContext);
            Likes = new LikeRepository(_dbContext);
            Tags = new TagRepository(_dbContext);
            Users = new UserRepository(_dbContext);
        }

        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
            => _dbContext.Entry(entity);

        public async Task<int> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();
    }
}
