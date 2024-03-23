using ICollection.DataAccess.Interfaces.Common;
using ICollection.Domain.Entities.Comments;
using ICollection.Service.Common.Exceptions;
using ICollection.Service.Common.Helpers;
using ICollection.Service.Dtos.Comments;
using ICollection.Service.Interfaces.Comments;
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
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateCommentAsync(CommentDto commentDto)
        {
            var comment = await _unitOfWork.Comments.FirstOrDefault(x => x.UserId == commentDto.UserId);
            if (comment == null)
            {
                var entity = new Comment
                {
                    Content = commentDto.CommentText,
                    UserId = commentDto.UserId,
                    ItemId = commentDto.ItemId,
                    CreatedAt = TimeHelper.GetCurrentServerTime()
                };
                var res = _unitOfWork.Comments.Add(entity);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            else throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "Comment is not created or already exists");
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

            _unitOfWork.Comments.Delete(id); // Pass the ID of the tweet to delete
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }
    }
}
