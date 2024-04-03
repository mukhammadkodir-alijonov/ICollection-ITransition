using ICollection.Domain.Entities.Items;

namespace ICollection.Service.Interfaces.Tags
{
    public interface ITagService
    {
        public Task CreateTagAsync(IEnumerable<string> tags, Item item);
        public Task UpdateTagAsync(IEnumerable<string> tags, Item itemToUpdate);
    }
}
