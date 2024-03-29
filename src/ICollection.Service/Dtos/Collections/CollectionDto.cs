using ICollection.Domain.Enums;
using ICollection.Service.Common.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Dtos.Collections
{
    public class CollectionDto
    {
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
