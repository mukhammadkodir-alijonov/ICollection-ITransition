using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Interfaces.Likes
{
    public interface ILikeService
    {
        public Task<bool> LikeCollectionAsync(int collectionId, int userId);
        public Task<bool> DislikeCollectionAsync(int collectionId, int userId);
        public Task<bool> LikeItemAsync(int itemId, int userId);
        public Task<bool> DislikeItemAsync(int itemId, int userId);
    }
}
