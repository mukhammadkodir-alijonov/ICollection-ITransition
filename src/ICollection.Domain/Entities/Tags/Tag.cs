using ICollection.Domain.Common;
using ICollection.Domain.Entities.Collections;
using ICollection.Domain.Entities.Items;

namespace ICollection.Domain.Entities.Tags
{
    public class Tag : Auditable
    {
        public string Name { get; set; } = string.Empty;

        public int ItemId { get; set; }
        public virtual List<Item> Items { get; set; } = default!;
    }
}
