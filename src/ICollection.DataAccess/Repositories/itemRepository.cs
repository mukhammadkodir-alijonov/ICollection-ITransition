using ICollection.DataAccess.DbContexts;
using ICollection.DataAccess.Interfaces;
using ICollection.DataAccess.Repositories.Common;
using ICollection.Domain.Entities.Items;

namespace ICollection.DataAccess.Repositories
{
    public class itemRepository : GenericRepository<Item>, IitemRepository
    {
        public itemRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
