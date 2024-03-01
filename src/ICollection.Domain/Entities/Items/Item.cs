using ICollection.Domain.Common;
using ICollection.Domain.Entities.Collections;
using ICollection.Domain.Entities.Comments;
using ICollection.Domain.Entities.Likes;
using ICollection.Domain.Entities.Tags;
using ICollection.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Domain.Entities.Items
{
    public class Item : Auditable
    {
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Dictionary<string, object>? CustomFieldValues { get; set; }

        public int TagId { get; set; }
        public virtual List<Tag> Tag { get; set; } = default!;

        public int CommentId { get; set; }
        public virtual List<Comment> Comments { get; set; } = default!;

        public int LikeId { get; set; }
        public virtual List<Like> Likes { get; set; } = default!;

        public int CollectionId { get; set; }
        public virtual Collection Collection { get; set; } = default!;

        public int UserId { get; set; }
        public virtual User User { get; set; } = default!;
    }
}
