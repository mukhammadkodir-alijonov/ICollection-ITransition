using DocumentFormat.OpenXml.Drawing.Wordprocessing;
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
        public async Task<bool> ToggleCollection(int collectionId)
        {
            var userid = _identityService.Id ?? 0;
            var likeCollection = await _unitOfWork.Likes.FirstOrDefault(x => x.CollectionId == collectionId && x.UserId == userid);
            if (likeCollection != null && userid == likeCollection?.UserId)
            {
                _unitOfWork.Likes.Delete(likeCollection.Id);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                var entity = new Like
                {
                    UserId = userid,
                    CollectionId = collectionId,
                    CreatedAt = TimeHelper.GetCurrentServerTime()
                };
                var res = _unitOfWork.Likes.Add(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            return true;
        }
        public async Task<bool> ToggleItem(int itemId)
        {
            var userid = _identityService.Id ?? 0;
            var likeItem = await _unitOfWork.LikeItem.FirstOrDefault(x => x.ItemId == itemId && x.UserId == userid);
            if (likeItem != null && userid == likeItem?.UserId)
            {
                _unitOfWork.LikeItem.Delete(likeItem.Id);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                var entity = new LikeItem
                {
                    UserId = userid,
                    ItemId = itemId,
                    CreatedAt = TimeHelper.GetCurrentServerTime()
                };
                var res = _unitOfWork.LikeItem.Add(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            return true;
        }
    }
}
