using ICollection.Domain.Entities.Items;
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
        public Task CreateTagAsync(IEnumerable<string> tags, Item item);
        public Task UpdateTagAsync(IEnumerable<string> tags, Item itemToUpdate);
    }
}
