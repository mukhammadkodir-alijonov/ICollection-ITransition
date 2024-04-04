using ICollection.DataAccess.Interfaces.Common;
using ICollection.Domain.Entities.Likes;
using ICollection.Service.Common.Exceptions;
using ICollection.Service.Common.Helpers;
using ICollection.Service.Interfaces.Common;
using ICollection.Service.Interfaces.Likes;

namespace ICollection.Service.Services.Likes
{
    public class LikeService : ILikeService
    {
        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;

        public LikeService(IUnitOfWork unitOfWork, IIdentityService identityService)
        {
            this._identityService = identityService;
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> LikeCollectionAsync(int collectionId)
        {
            var userid = _identityService.Id ?? 0;
            var likeCollection = await _unitOfWork.Likes.FirstOrDefault(x => x.CollectionId == collectionId && x.UserId == userid);
            if (likeCollection == null)
            {
                var entity = new Like
                {
                    UserId = userid,
                    CollectionId = collectionId,
                    CreatedAt = TimeHelper.GetCurrentServerTime()
                };
                var res = _unitOfWork.Likes.Add(entity);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            else throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "Collection is already liked");
        }
        public async Task<bool> LikeItemAsync(int itemId)
        {
            var userid = _identityService.Id ?? 0;
            var likeItem = await _unitOfWork.LikeItem.FirstOrDefault(x => x.ItemId == itemId && x.UserId == userid);
            if (likeItem == null)
            {
                var entity = new LikeItem
                {
                    UserId = userid,
                    ItemId = itemId,
                    CreatedAt = TimeHelper.GetCurrentServerTime()
                };
                var res = _unitOfWork.LikeItem.Add(entity);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            else throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "Item is already liked");
        }
        public async Task<bool> DislikeCollectionAsync(int collectionId)
        {
            var userid = _identityService.Id ?? 0;
            var unlike = await _unitOfWork.Likes.FirstOrDefault(x => x.CollectionId == collectionId && x.UserId == userid);
            if (userid == unlike?.UserId && unlike != null)
            {
                _unitOfWork.Likes.Delete(unlike.Id);
                return 0 < await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "Collection is not liked");
            }
        }
        public async Task<bool> DislikeItemAsync(int itemId)
        {
            var userid = _identityService.Id ?? 0;
            var unlike = await _unitOfWork.LikeItem.FirstOrDefault(x => x.ItemId == itemId && x.UserId == userid);
            if (userid == unlike?.UserId && unlike != null)
            {
                _unitOfWork.LikeItem.Delete(unlike.Id);
                return 0 < await _unitOfWork.SaveChangesAsync();
            }
            else
                throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "Item is not liked");
        }
    }
}
