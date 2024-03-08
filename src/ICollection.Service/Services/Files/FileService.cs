using ICollection.Service.Dtos.Files;
using ICollection.Service.Interfaces.Files;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Files
{
    public class FileService : IFileService
    {
        public Task<string> CreateFile(FileModelDto filemodel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFileAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteImageAsync(string imagePartPath)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadImageAsync(IFormFile image)
        {
            throw new NotImplementedException();
        }
    }
}
