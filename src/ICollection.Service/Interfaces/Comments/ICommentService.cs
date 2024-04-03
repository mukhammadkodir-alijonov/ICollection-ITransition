using ICollection.Service.Dtos.Comments;

namespace ICollection.Service.Interfaces.Comments
{
    public interface ICommentService
    {
        public Task<bool> CreateCommentAsync(CommentDto commentDto);
        public Task<bool> DeleteCommentAsync(int id);
    }
}
