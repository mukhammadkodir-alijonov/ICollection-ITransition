using ICollection.Domain.Common;
using ICollection.Domain.Entities.Collections;
using ICollection.Domain.Entities.Items;
using ICollection.Domain.Entities.Users;

namespace ICollection.Domain.Entities.Likes
{
    public class Like : Auditable
    {
        public int ItemId { get; set; }
        public virtual Item Item { get; set; } = default!;

        public int CollectionId { get; set; }
        public virtual Collection Collection { get; set; } = default!;

        public int UserId { get; set; }
        public virtual User User { get; set; } = default!;
    }
}
