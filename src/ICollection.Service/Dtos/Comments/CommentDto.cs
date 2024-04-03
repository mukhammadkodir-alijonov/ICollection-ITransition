using ICollection.Service.Common.Attributes;
using System.ComponentModel.DataAnnotations;

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
