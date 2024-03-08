using ICollection.Service.Dtos.Tags;
using ICollection.Service.Interfaces.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Tags
{
    public class TagService : ITagService
    {
        public Task<bool> CreateTagAsync(TagDto tag)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTagAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
