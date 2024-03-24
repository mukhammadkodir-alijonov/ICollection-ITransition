using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Items;
using ICollection.Service.ViewModels.CollectionViewModels;
using ICollection.Service.ViewModels.ItemViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Interfaces.Items
{
    public interface IitemService
    { 
        public Task<PagedList<ItemViewModel>> GetAllItemAsync(PaginationParams @params);
        //public Task<List<LikePerItemViewModel>> GetAllLikeByItemAsync(int collectionId);
        public Task<bool> CreateItemAsync(ItemDto item);
        public Task<bool> UpdateItemAsync(int id,ItemDto item);
        public Task<bool> DeleteItemAsync(int id);
    }
}
