using ICollection.Domain.Common;
using ICollection.Domain.Entities.Items;
using ICollection.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Domain.Entities.Likes
{
    public class LikeItem: Auditable
    {
        public int ItemId { get; set; }
        public virtual Item Item { get; set; } = default!;

        public int UserId { get; set; }
        public virtual User User { get; set; } = default!;
    }
}
