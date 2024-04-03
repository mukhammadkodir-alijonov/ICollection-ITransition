using AutoMapper;
using ICollection.DataAccess.Interfaces.Common;
using ICollection.DataAccess.Repositories.Common;
using ICollection.Domain.Entities.Collections;
using ICollection.Service.Common.Exceptions;
using ICollection.Service.Common.Helpers;
using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Admins;
using ICollection.Service.Dtos.Collections;
using ICollection.Service.Dtos.CustomFields;
using ICollection.Service.Interfaces.Collections;
using ICollection.Service.Interfaces.Common;
using ICollection.Service.Interfaces.Files;
using ICollection.Service.ViewModels.CollectionViewModels;
using Microsoft.AspNetCore.Http;
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
        private readonly IImageService _imageService;
        private readonly IFileService _fileService;
        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CollectionService(IUnitOfWork unitOfWork, IMapper mapper, IIdentityService identityService, IFileService fileService, IImageService imageService)
        {
            this._imageService = imageService;
            this._fileService = fileService;
            this._identityService = identityService;
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
            var userid = _identityService.Id ?? 0;
            var imagepath = await _imageService.SaveImageAsync(collectionCreateDto.Image!);
            var entity = new Collection
            {
                Name = collectionCreateDto.Name,
                Description = collectionCreateDto.Description,
                Topics = collectionCreateDto.Topics,
                Image = imagepath,
                UserId = userid,
                CreatedAt = TimeHelper.GetCurrentServerTime(),
                LastUpdatedAt = TimeHelper.GetCurrentServerTime()
            };
            var res = _unitOfWork.Collections.Add(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
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

        public async Task<bool> UpdateCollectionAsync(int id, CollectionUpdateDto collectionUpdateDto)
        {
            var collection = await _unitOfWork.Collections.FindByIdAsync(id);
            if (collection is null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Collection not found");
            collection.Name = collectionUpdateDto.Name;
            collection.Description = collectionUpdateDto.Description;
            collection.Topics = collectionUpdateDto.Topics;
            collection.Image = String.IsNullOrEmpty(collectionUpdateDto.ImagePath) ? collection.Image : collectionUpdateDto.ImagePath;
            if (collectionUpdateDto.Image is not null)
            {
                collection.Image = await _fileService.UploadImageAsync(collectionUpdateDto.Image);
            }
            collection.LastUpdatedAt = TimeHelper.GetCurrentServerTime();
            _unitOfWork.Collections.Update(id, collection);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }
        public async Task<bool> UpdateImageAsync(int id, IFormFile formFile)
        {
            var admin = await _unitOfWork.Collections.FindByIdAsync(id);
            var updateImage = await _fileService.UploadImageAsync(formFile);
            var collectionUpdateDto = new CollectionUpdateDto()
            {
                ImagePath = updateImage
            };
            var result = await UpdateCollectionAsync(id, collectionUpdateDto);
            return result;
        }
        public async Task<PagedList<CollectionViewModel>> SearchAsync(PaginationParams @params, string name)
        {
            //var query = _unitOfWork.Collections.GetAll().Where(x => x.Name.ToLower().StartsWith(name.ToLower()) || x.Description.ToLower().StartsWith(name.ToLower())).OrderByDescending(x => x.CreatedAt).Select(x => _mapper.Map<CollectionViewModel>(x));
            //return await PagedList<CollectionViewModel>.ToPagedListAsync(query, @params);
            var query = from collection in _unitOfWork.Collections.GetAll()
                        where collection.Name.ToLower().StartsWith(name.ToLower()) || collection.Description.ToLower().StartsWith(name.ToLower())
                        let likeCount = _unitOfWork.Likes.GetAll().Where(x => x.CollectionId == collection.Id).Count()
                        orderby likeCount descending
                        select new CollectionViewModel()
                        {
                            Id = collection.Id,
                            Name = collection.Name,
                            Description = collection.Description,
                            Topics = collection.Topics,
                            ImagePath = collection.Image,
                            UserId = collection.UserId,
                            CreatedAt = collection.CreatedAt,
                            LastUpdatedAt = collection.LastUpdatedAt,
                            LikeCount = likeCount
                        };
            return await PagedList<CollectionViewModel>.ToPagedListAsync(query, @params);
        }
        public async Task<PagedList<CollectionViewModel>> TopCollection(PaginationParams @params)
        {
            var userid = _identityService.Id ?? 0;
            var query = from collection in _unitOfWork.Collections.GetAll()
                        let likeCount = _unitOfWork.Likes.GetAll().Where(x => x.CollectionId == collection.Id).Count()
                        orderby likeCount descending
                        select new CollectionViewModel()
                        {
                            Id = collection.Id,
                            Name = collection.Name,
                            Description = collection.Description,
                            Topics = collection.Topics,
                            ImagePath = collection.Image,
                            UserId = userid,
                            CreatedAt = collection.CreatedAt,
                            LastUpdatedAt = collection.LastUpdatedAt,
                            LikeCount = likeCount
                        };
            return await PagedList<CollectionViewModel>.ToPagedListAsync(query, @params);
        }
        public async Task<bool> GetCollectionById(int userId,int collectionId)
        {
            var res = await _unitOfWork.Collections.FindByIdAsync(collectionId);
            if (res is null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Collection not found");
            return res.UserId == userId;
        }
    }
}
