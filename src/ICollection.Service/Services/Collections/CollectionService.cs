using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Collections;
using ICollection.Service.Interfaces.Collections;
using ICollection.Service.ViewModels.CollectionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Collections
{
    public class CollectionService : ICollectionService
    {
        public Task<bool> CreateCollectionAsync(CollectionDto collectionCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCollectionAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<CollectionViewModel>> GetAllCollectionAsync(PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<List<LikePerCollectionViewModel>> GetAllLikeByCollectionAsync(int collectionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCollectionAsync(int id, CollectionDto collectionUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
