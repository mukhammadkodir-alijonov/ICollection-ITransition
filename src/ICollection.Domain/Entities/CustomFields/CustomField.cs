using ICollection.Domain.Common;
using ICollection.Domain.Entities.Collections;
using ICollection.Domain.Entities.Items;
using ICollection.Domain.Enums;

namespace ICollection.Domain.Entities.CustomFields
{
    public class CustomField : Auditable
    {
        public string Name { get; set; } = string.Empty;
        public FieldType Type { get; set; } = FieldType.String;

        public int CollectionId { get; set; }
        public virtual Collection Collection { get; set; } = default!;

        public int ItemId { get; set; }
        public virtual Item Item { get; set; } = default!;
    }
}
