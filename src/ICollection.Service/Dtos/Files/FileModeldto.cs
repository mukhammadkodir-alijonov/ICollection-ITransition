using ICollection.Service.Common.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ICollection.Service.Dtos.Files
{
    public class FileModelDto
    {
        [AllowedFiles(new string[] { ".xlsx" })]
        [Required]
        public IFormFile? File { get; set; }
    }
}
