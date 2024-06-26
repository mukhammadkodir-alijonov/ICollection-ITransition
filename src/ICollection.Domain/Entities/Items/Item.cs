﻿using ICollection.Domain.Common;
using ICollection.Domain.Entities.Collections;
using ICollection.Domain.Entities.Tags;
using ICollection.Domain.Entities.Users;

namespace ICollection.Domain.Entities.Items
{
    public class Item : Auditable
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;

        public int TagId { get; set; }
        public virtual List<Tag> Tags { get; set; } = default!;

        public int CollectionId { get; set; }
        public virtual Collection Collection { get; set; } = default!;

        public int UserId { get; set; }
        public virtual User User { get; set; } = default!;
    }
}
