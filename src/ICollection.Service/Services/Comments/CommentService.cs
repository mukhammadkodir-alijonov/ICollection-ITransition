using ICollection.Service.Dtos.Comments;
using ICollection.Service.Interfaces.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Comments
{
    public class CommentService : ICommentService
    {
        public async Task<bool> CreateCommentAsync(CommentDto commentDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
