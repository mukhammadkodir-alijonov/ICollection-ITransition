using ICollection.Service.Dtos.Files;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Interfaces.Files
{
    public interface IFileService
    {
        public Task<string> CreateFile(FileModeldto filemodel);
        public Task<bool> DeleteFileAsync(string path);
        public Task<string> UploadImageAsync(IFormFile image);
        public Task<bool> DeleteImageAsync(string imagePartPath);
    }
}
