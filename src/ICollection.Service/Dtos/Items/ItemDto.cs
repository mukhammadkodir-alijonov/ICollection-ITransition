using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Dtos.Items
{
    public class ItemDto
    {
        public int ItemId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public Dictionary<string, object>? CustomFieldValues { get; set; }
        public int CollectionId { get; set; }
    }
}
