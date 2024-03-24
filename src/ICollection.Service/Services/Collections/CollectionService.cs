using AutoMapper;
using ICollection.DataAccess.Interfaces.Common;
using ICollection.DataAccess.Repositories.Common;
using ICollection.Domain.Entities.Collections;
using ICollection.Service.Common.Exceptions;
using ICollection.Service.Common.Helpers;
using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Collections;
using ICollection.Service.Interfaces.Collections;
using ICollection.Service.ViewModels.CollectionViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Collections
{
    public class CollectionService : ICollectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CollectionService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<PagedList<CollectionViewModel>> GetAllCollectionAsync(PaginationParams @params)
        {
            var query = _unitOfWork.Collections.GetAll().OrderBy(x => x.Id)
                .Select(x => _mapper.Map<CollectionViewModel>(x));
            return await PagedList<CollectionViewModel>.ToPagedListAsync(query, @params);
        }

        public async Task<bool> CreateCollectionAsync(CollectionDto collectionCreateDto)
        {
            var collection = await _unitOfWork.Collections.FirstOrDefault(x => x.UserId == collectionCreateDto.UserId);
            if (collection == null)
            {
                var entity = new Collection
                {
                    Name = collectionCreateDto.Name,
                    Description = collectionCreateDto.Description,
                    Topics = collectionCreateDto.Topics,
                    ImagePath = collectionCreateDto.ImagePath,
                    UserId = collectionCreateDto.UserId,
                    CreatedAt = TimeHelper.GetCurrentServerTime(),
                    LastUpdatedAt = TimeHelper.GetCurrentServerTime()
                };
                var res = _unitOfWork.Collections.Add(entity);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            else throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "Collection is not created or already exists");
        }

        public async Task<bool> DeleteCollectionAsync(int id)
        {
            var collection = await _unitOfWork.Collections.FindByIdAsync(id);
            if (collection is null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Collection not found");
            _unitOfWork.Collections.Delete(id);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateCollectionAsync(int id, CollectionDto collectionUpdateDto)
        {
            var collection = await _unitOfWork.Collections.FindByIdAsync(id);
            if (collection is null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Collection not found");
            collection.Name = collectionUpdateDto.Name;
            collection.Description = collectionUpdateDto.Description;
            collection.Topics = collectionUpdateDto.Topics;
            collection.ImagePath = collectionUpdateDto.ImagePath;
            collection.LastUpdatedAt = TimeHelper.GetCurrentServerTime();
            _unitOfWork.Collections.Update(id, collection);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }
    }
}
