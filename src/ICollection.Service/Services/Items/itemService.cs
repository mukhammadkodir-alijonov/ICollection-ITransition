using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Items;
using ICollection.Service.Interfaces.Items;
using ICollection.Service.ViewModels.ItemViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Items
{
    public class itemService : IitemService
    {
        public Task<bool> CreateItemAsync(ItemDto item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<ItemViewModel>> GetAllItemAsync(PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<List<LikePerItemViewModel>> GetAllLikeByItemAsync(int collectionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(int id, ItemDto item)
        {
            throw new NotImplementedException();
        }
    }
}
