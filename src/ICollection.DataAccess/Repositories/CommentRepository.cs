using ICollection.DataAccess.DbContexts;
using ICollection.DataAccess.Interfaces;
using ICollection.DataAccess.Repositories.Common;
using ICollection.Domain.Entities.Comments;

namespace ICollection.DataAccess.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
