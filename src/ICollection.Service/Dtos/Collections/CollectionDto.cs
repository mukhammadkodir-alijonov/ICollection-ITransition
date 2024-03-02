using ICollection.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Dtos.Collections
{
    public class CollectionDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Topics Topics { get; set; } = Topics.Other;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
