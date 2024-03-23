using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICollection.Service.Common.Attributes;
using ICollection.Service.Dtos.Comments;

namespace ICollection.Service.Dtos.Comments
{
    public class CommentDto
    {
        [Comment]
        public string CommentText { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int ItemId { get; set; }
    }
}
