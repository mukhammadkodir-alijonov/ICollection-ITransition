using DocumentFormat.OpenXml.Spreadsheet;
using ICollection.DataAccess.Interfaces.Common;
using ICollection.Domain.Entities.Likes;
using ICollection.Service.Common.Exceptions;
using ICollection.Service.Common.Helpers;
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
        private readonly IUnitOfWork _unitOfWork;

        public LikeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> DislikeCollectionAsync(int collectionId, int userId)
        {
            var unlike = _unitOfWork.Likes.FirstOrDefault(x => x.CollectionId == collectionId && x.UserId == userId);
            if (unlike != null)
            {
                _unitOfWork.Likes.Delete(unlike.Id);
                return 0 < await _unitOfWork.SaveChangesAsync();
            }
            else 
                throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "Collection is not liked");
        }

        public async Task<bool> DislikeItemAsync(int itemId, int userId)
        {
            var unlike = _unitOfWork.Likes.FirstOrDefault(x => x.ItemId == itemId && x.UserId == userId);
            if (unlike != null)
            {
                _unitOfWork.Likes.Delete(unlike.Id);
                return 0 < await _unitOfWork.SaveChangesAsync();
            }
            else 
                throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "Item is not liked");
        }

        public async Task<bool> LikeCollectionAsync(int collectionId, int userId)
        {
            var likeCollection = await _unitOfWork.Likes.FirstOrDefault(x => x.CollectionId == collectionId && x.UserId == userId);
            if (likeCollection == null)
            {
                var entity = new Like
                {
                    UserId = userId,
                    CollectionId = collectionId,
                    CreatedAt = TimeHelper.GetCurrentServerTime()
                };
                var res = _unitOfWork.Likes.Add(entity);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            else throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "Collection is already liked");
        }

        public async Task<bool> LikeItemAsync(int itemId, int userId)
        {
            var likeItem = await _unitOfWork.Likes.FirstOrDefault(x => x.ItemId == itemId && x.UserId == userId);
            if (likeItem == null)
            {
                var entity = new Like
                {
                    UserId = userId,
                    ItemId = itemId,
                    CreatedAt = TimeHelper.GetCurrentServerTime()
                };
                var res = _unitOfWork.Likes.Add(entity);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            else throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "Item is already liked");
        }
    }
}
