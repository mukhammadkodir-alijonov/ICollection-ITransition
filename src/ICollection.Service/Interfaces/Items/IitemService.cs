using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Items;
using ICollection.Service.ViewModels.ItemViewModels;

namespace ICollection.Service.Interfaces.Items
{
    public interface IitemService
    {
        public Task<PagedList<ItemViewModel>> GetAllItemAsync(int id, PaginationParams @params);
        //public Task<List<LikePerItemViewModel>> GetAllLikeByItemAsync(int collectionId);
        public Task<bool> CreateItemAsync(ItemDto item);
        public Task<bool> UpdateItemAsync(int id, ItemUpdateDto item);
        public Task<bool> DeleteItemAsync(int id);
    }
}
