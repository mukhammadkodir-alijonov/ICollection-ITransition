using ICollection.DataAccess.DbContexts;
using ICollection.DataAccess.Interfaces;
using ICollection.DataAccess.Repositories.Common;
using ICollection.Domain.Entities.CustomFields;

namespace ICollection.DataAccess.Repositories
{
    public class CustomFieldRepository : GenericRepository<CustomField>, ICustomFieldRepository
    {
        public CustomFieldRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
