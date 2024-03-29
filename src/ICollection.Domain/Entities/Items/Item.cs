using ICollection.Domain.Common;
using ICollection.Domain.Entities.Collections;
using ICollection.Domain.Entities.Comments;
using ICollection.Domain.Entities.CustomFields;
using ICollection.Domain.Entities.Likes;
using ICollection.Domain.Entities.Tags;
using ICollection.Domain.Entities.Users;

namespace ICollection.Domain.Entities.Items
{
    public class Item : Auditable
    {
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int CostomFieldId { get; set; }
        public virtual List<CustomField> CustomFields { get; set; } = default!;

        public int TagId { get; set; }
        public virtual List<Tag> Tags { get; set; } = default!;

        public int CommentId { get; set; }
        public virtual List<Comment> Comments { get; set; } = default!;

        public int CollectionId { get; set; }
        public virtual Collection Collection { get; set; } = default!;

        public int UserId { get; set; }
        public virtual User User { get; set; } = default!;
    }
}
