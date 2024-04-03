namespace ICollection.Service.Interfaces.Likes
{
    public interface ILikeService
    {
        public Task<bool> LikeCollectionAsync(int collectionId);
        public Task<bool> LikeItemAsync(int itemId);
        public Task<bool> DislikeCollectionAsync(int collectionId);
        public Task<bool> DislikeItemAsync(int itemId);
    }
}
