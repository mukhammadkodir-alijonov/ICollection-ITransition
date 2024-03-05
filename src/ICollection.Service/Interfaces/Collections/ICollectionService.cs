using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Collections;
using ICollection.Service.ViewModels.CollectionViewModels;
using ICollection.Service.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Interfaces.Collections
{
    public interface ICollectionService
    {
        public Task<PagedList<CollectionViewModel>> GetAllAsync(PaginationParams @params);
        public Task<bool> CreateCollectionAsync(CollectionDto collectionCreateDto);
        public Task<bool> DeleteCollectionAsync(int id);
        public Task<bool> UpdateCollectionAsync(int id, CollectionDto collectionUpdateDto);
    }
}
