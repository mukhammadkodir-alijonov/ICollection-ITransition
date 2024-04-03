using ICollection.Service.Dtos.Files;
using Microsoft.AspNetCore.Http;

namespace ICollection.Service.Interfaces.Files
{
    public interface IFileService
    {
        public Task<string> CreateFile(FileModelDto filemodel);
        public Task<bool> DeleteFileAsync(string path);
        public Task<string> UploadImageAsync(IFormFile image);
        public Task<bool> DeleteImageAsync(string imagePartPath);
    }
}
