using ICollection.Service.Dtos.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Interfaces.Tags
{
    public interface ITagService
    {
        public Task<bool> CreateTagAsync(TagDto tag);
        public Task<bool> DeleteTagAsync(int id);
    }
}
