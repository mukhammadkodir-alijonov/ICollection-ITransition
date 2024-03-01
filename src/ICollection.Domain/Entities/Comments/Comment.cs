using ICollection.Domain.Common;
using ICollection.Domain.Entities.Items;
using ICollection.Domain.Entities.Users;

namespace ICollection.Domain.Entities.Comments
{
    public class Comment : Auditable
    {
        public string Content { get; set; } = string.Empty;

        public int ItemId { get; set; }
        public virtual Item Item { get; set; } = default!;

        public int UserId { get; set; }
        public virtual User User { get; set; } = default!;
    }
}
