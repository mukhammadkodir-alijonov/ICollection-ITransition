using Microsoft.AspNetCore.Http;
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
        [Required]
        public string Name { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        public Dictionary<string, object>? CustomFieldValues { get; set; }
        public int CollectionId { get; set; }
    }
}
