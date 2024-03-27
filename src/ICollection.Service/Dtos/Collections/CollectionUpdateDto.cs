using DocumentFormat.OpenXml.Spreadsheet;
using ICollection.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Dtos.Collections
{
    public class CollectionUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Topics Topics { get; set; } = Topics.Other;
        public string CustomFields { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public int CostomFieldId { get; set; }
    }
}
