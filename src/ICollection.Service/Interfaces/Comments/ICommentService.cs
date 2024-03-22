using ICollection.Service.Dtos.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Interfaces.Comments
{
    public interface ICommentService
    {
        public Task<bool> CreateCommentAsync(CommentDto commentDto);
        public Task<bool> DeleteCommentAsync(int id);
    }
}
