using ICollection.DataAccess.Interfaces.Common;
using ICollection.Domain.Entities.Tags;
using ICollection.Service.Common.Exceptions;
using ICollection.Service.Dtos.Tags;
using ICollection.Service.Interfaces.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Tags
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateTagAsync(TagDto tagDto)
        {
            var tag = await _unitOfWork.Tags.FirstOrDefault(x => x.Id == tagDto.TagId);
            if (tag == null)
            {
                var entity = new Tag
                {
                    Name = tagDto.Name,
                    CollectionId = tagDto.CollectionId
                };
                var res = _unitOfWork.Tags.Add(entity);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            else throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "Tag is not created");
        }

        public async Task<bool> DeleteTagAsync(int id)
        {
            var tag = await _unitOfWork.Tags.FindByIdAsync(id);
            if (tag is null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Tag not found");
            _unitOfWork.Tags.Delete(id);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
