using ICollection.DataAccess.Interfaces.Common;
using ICollection.Domain.Entities.Comments;
using ICollection.Service.Common.Exceptions;
using ICollection.Service.Common.Helpers;
using ICollection.Service.Dtos.Comments;
using ICollection.Service.Interfaces.Comments;
using ICollection.Service.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork, IIdentityService identityService)
        {
            this._identityService = identityService;
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateCommentAsync(CommentDto commentDto)
        {
            var userid = _identityService.Id ?? 0;
            var entity = new Comment
            {
                Content = commentDto.CommentText,
                UserId = userid,
                ItemId = commentDto.ItemId,
                CreatedAt = TimeHelper.GetCurrentServerTime()
            };
            var res = _unitOfWork.Comments.Add(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid tweet ID");
            }

            var tweetToDelete = await _unitOfWork.Comments.FindByIdAsync(id);

            if (tweetToDelete == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Tweet not found");
            }
            var userid = _identityService.Id ?? 0;
            var userinitem = await _unitOfWork.Comments.FindByIdAsync(id);
            if (userid == userinitem?.UserId)
            {
                _unitOfWork.Comments.Delete(id); // Pass the ID of the tweet to delete
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0;
            }
            else
                throw new StatusCodeException(HttpStatusCode.BadRequest, "You are not authorized to delete this comment");
        }
    }
}
