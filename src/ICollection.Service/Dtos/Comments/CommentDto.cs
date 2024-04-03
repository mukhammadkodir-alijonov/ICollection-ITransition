using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICollection.Service.Common.Attributes;
using ICollection.Service.Dtos.Comments;

namespace ICollection.Service.Dtos.Comments
{
    public class CommentDto
    {
        public int Id { get; set; } 
        [Comment]
        [Required]
        public string CommentText { get; set; } = string.Empty;
        public int ItemId { get; set; }
    }
}
