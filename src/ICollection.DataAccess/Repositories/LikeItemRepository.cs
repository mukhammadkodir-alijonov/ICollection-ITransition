using ICollection.DataAccess.DbContexts;
using ICollection.DataAccess.Interfaces;
using ICollection.DataAccess.Repositories.Common;
using ICollection.Domain.Entities.Likes;

namespace ICollection.DataAccess.Repositories
{
    public class LikeItemRepository : GenericRepository<LikeItem>, ILikeItemRepository
    {
        public LikeItemRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
