using ICollection.DataAccess.Interfaces.Common;
using ICollection.Domain.Entities.Items;
using ICollection.Domain.Entities.Tags;
using ICollection.Service.Interfaces.Tags;

namespace ICollection.Service.Services.Tags
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task CreateTagAsync(IEnumerable<string> tags, Item item)
        {
            item.Tags = new List<Tag>();
            foreach (var tag in tags)
            {
                if (int.TryParse(tag, out var tagId))
                {
                    var dbTag = await _unitOfWork.Tags.FindByIdAsync(tagId);
                    if (dbTag != null) item.Tags.Add(dbTag);
                    continue;
                }
                item.Tags.Add(new Tag { Name = tag });
            }
        }

        public async Task UpdateTagAsync(IEnumerable<string> tags, Item itemToUpdate)
        {
            if (!tags.Any())
            {
                itemToUpdate.Tags = new List<Tag>();
                return;
            }
            await CreateTagAsync(tags, itemToUpdate);
        }
    }
}
