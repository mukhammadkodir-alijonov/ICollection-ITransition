using AutoMapper;
using ICollection.DataAccess.Interfaces.Common;
using ICollection.Domain.Entities.Items;
using ICollection.Service.Common.Exceptions;
using ICollection.Service.Common.Helpers;
using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Items;
using ICollection.Service.Interfaces.Common;
using ICollection.Service.Interfaces.Files;
using ICollection.Service.Interfaces.Items;
using ICollection.Service.ViewModels.ItemViewModels;
using Microsoft.AspNetCore.Http;

namespace ICollection.Service.Services.Items
{
    public class itemService : IitemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IImageService _imageService;
        private readonly IIdentityService _identityService;

        public itemService(IUnitOfWork unitOfWork, IMapper mapper, IIdentityService identityService, IImageService imageService, IFileService fileService)
        {
            this._fileService = fileService;
            this._imageService = imageService;
            this._identityService = identityService;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<bool> CreateItemAsync(ItemDto itemDto)
        {
            var userid = _identityService.Id ?? 0;
            var userincollection = await _unitOfWork.Collections.FindByIdAsync(itemDto.CollectionId);
            if (userid == userincollection?.UserId)
            {
                var imagepath = await _imageService.SaveImageAsync(itemDto.Image!);
                var entity = new Item
                {
                    Name = itemDto.Name,
                    Description = itemDto.Description,
                    UserId = userid,
                    Image = imagepath,
                    CollectionId = itemDto.CollectionId
                };
                var res = _unitOfWork.Iitems.Add(entity);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            else
                throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "You are not authorized to create an item in this collection");
        }
        public async Task<bool> DeleteItemAsync(int id)
        {
            var userid = _identityService.Id ?? 0;
            var userinitem = await _unitOfWork.Iitems.FindByIdAsync(id);
            if (userid == userinitem?.UserId)
            {
                var item = await _unitOfWork.Iitems.FindByIdAsync(id);
                if (item is null)
                    throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Item not found");
                _unitOfWork.Iitems.Delete(id);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0;
            }
            else
                throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "You are not authorized to delete this item");

        }
        public async Task<PagedList<ItemViewModel>> GetAllItemAsync(int id, PaginationParams @params)
        {
            var query = (from item in _unitOfWork.Iitems.GetAll()
                        .Where(x => x.CollectionId == id).OrderBy(x => x.Id)
                         let isLiked = _unitOfWork.LikeItem.GetAll().Any(x => x.ItemId == item.Id)
                         let likeCount = _unitOfWork.LikeItem.GetAll().Where(x => x.ItemId == item.Id).Count()
                         orderby likeCount descending
                         select new ItemViewModel
                         {
                             Id = item.Id,
                             Name = item.Name,
                             Description = item.Description,
                             ImagePath = item.Image,
                             LikeCount = likeCount,
                             CollectionId = item.CollectionId,
                             IsLiked = isLiked != null
                         });
            return await PagedList<ItemViewModel>.ToPagedListAsync(query, @params);
        }
        public async Task<bool> UpdateItemAsync(int id, ItemUpdateDto item)
        {
            var userid = _identityService.Id ?? 0;
            var userinitem = await _unitOfWork.Iitems.FindByIdAsync(id);
            if (userid == userinitem?.UserId)
            {
                var entity = await _unitOfWork.Iitems.FindByIdAsync(id);
                if (entity is null)
                    throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Item not found");
                entity.Name = item.Name;
                entity.Description = item.Description;
                entity.Image = String.IsNullOrEmpty(item.ImagePath) ? entity.Image : item.ImagePath;
                if (item.Image is not null)
                {
                    entity.Image = await _fileService.UploadImageAsync(item.Image);
                }
                entity.LastUpdatedAt = TimeHelper.GetCurrentServerTime();
                _unitOfWork.Iitems.Update(id, entity);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0;
            }
            else
                throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "You are not authorized to update this item");
        }
        public async Task<bool> UpdateImageAsync(int id, IFormFile formFile)
        {
            var admin = await _unitOfWork.Collections.FindByIdAsync(id);
            var updateImage = await _fileService.UploadImageAsync(formFile);
            var collectionUpdateDto = new ItemUpdateDto()
            {
                ImagePath = updateImage
            };
            var result = await UpdateItemAsync(id, collectionUpdateDto);
            return result;
        }
    }
}
