using ICollection.Service.Interfaces.Likes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Likes
{
    public class LikeService : ILikeService
    {
        public Task<bool> DislikeCollectionAsync(int collectionId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DislikeItemAsync(int itemId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LikeCollectionAsync(int collectionId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LikeItemAsync(int itemId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
