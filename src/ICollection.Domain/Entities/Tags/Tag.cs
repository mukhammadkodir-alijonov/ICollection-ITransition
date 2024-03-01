using ICollection.Domain.Common;
using ICollection.Domain.Entities.Collections;

namespace ICollection.Domain.Entities.Tags
{
    public class Tag : Auditable
    {
        public string Name { get; set; } = string.Empty;

        public int CollectionId { get; set; }
        public virtual List<Collection> Collection { get; set; } = default!;
    }
}
