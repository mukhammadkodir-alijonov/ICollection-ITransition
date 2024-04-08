namespace ICollection.Service.Interfaces.Likes
{
    public interface ILikeService
    {
        public Task<bool> ToggleCollection(int collectionId);
        public Task<bool> ToggleItem(int itemId);
    }
}
