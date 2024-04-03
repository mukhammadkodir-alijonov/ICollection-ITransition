using ICollection.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ICollection.Service.Dtos.Collections
{
    public class CollectionDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public Topics Topics { get; set; } = Topics.Other;
        public IFormFile? Image { get; set; }
        public int CustomFieldId { get; set; }
        public Dictionary<string, object>? CustomFieldValues { get; set; }
    }
}
