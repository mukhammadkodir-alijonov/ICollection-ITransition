using ICollection.Service.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Common
{
    public class ImageService : IImageService
    {
        public Task<bool> DeleteImageAsync(string imagePath)
        {
            throw new NotImplementedException();
        }

        public Task<string> SaveImageAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
