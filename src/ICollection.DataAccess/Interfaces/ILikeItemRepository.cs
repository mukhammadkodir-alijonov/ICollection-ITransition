using ICollection.DataAccess.Interfaces.Common;
using ICollection.Domain.Entities.Likes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.DataAccess.Interfaces
{
    public interface ILikeItemRepository : IGenericRepository<LikeItem>
    {
    }
}
