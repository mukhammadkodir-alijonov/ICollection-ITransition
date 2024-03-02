using ICollection.Domain.Common;
using ICollection.Domain.Entities.CustomFields;
using ICollection.Domain.Entities.Items;
using ICollection.Domain.Entities.Users;
using ICollection.Domain.Enums;

namespace ICollection.Domain.Entities.Collections
{
    public class Collection : Auditable
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Topics Topics { get; set; } = Topics.Other;
        public string ImagePath { get; set; } = string.Empty;

        public int ItemId { get; set; }
        public virtual List<Item> Items { get; set; } = default!;

        public int UserId { get; set; }
        public virtual User User { get; set; } = default!;

        public int CostomFieldId { get; set; }
        public virtual List<CustomField> CustomFields { get; set; } = default!;
    }
}
