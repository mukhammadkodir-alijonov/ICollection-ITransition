using AutoMapper;
using ICollection.DataAccess.Interfaces.Common;
using ICollection.Domain.Entities.CustomFields;
using ICollection.Domain.Entities.Items;
using ICollection.Service.Common.Exceptions;
using ICollection.Service.Common.Helpers;
using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.CustomFields;
using ICollection.Service.Dtos.Items;
using ICollection.Service.Interfaces.Common;
using ICollection.Service.Interfaces.Items;
using ICollection.Service.ViewModels.CollectionViewModels;
using ICollection.Service.ViewModels.ItemViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Items
{
    public class itemService : IitemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly IIdentityService _identityService;

        public itemService(IUnitOfWork unitOfWork,IMapper mapper,IIdentityService identityService,IImageService imageService)
        {
            this._imageService = imageService;
            this._identityService = identityService;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<bool> CreateItemAsync(ItemDto itemDto)
        {
            var userid = _identityService.Id ?? 0;
            var imagepath = await _imageService.SaveImageAsync(itemDto.Image!);
                var entity = new Item
                {
                    Name = itemDto.Name,
                    Description = itemDto.Description,
                    UserId = userid,
                    ImagePath = imagepath,
                    CollectionId = itemDto.CollectionId
                };
                var res = _unitOfWork.Iitems.Add(entity);
                return await _unitOfWork.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteItemAsync(int id)
        {
            var item = await _unitOfWork.Iitems.FindByIdAsync(id);
            if (item is null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Item not found");
            _unitOfWork.Iitems.Delete(id);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }
        public async Task<PagedList<ItemViewModel>> GetAllItemAsync(int id,PaginationParams @params)
        {
            var query = _unitOfWork.Iitems.GetAll().Where(x=>x.CollectionId == id).OrderBy(x => x.Id)
                        .Select(x => _mapper.Map<ItemViewModel>(x));
            return await PagedList<ItemViewModel>.ToPagedListAsync(query, @params);
        }
        public async Task<bool> UpdateItemAsync(int id, ItemDto item)
        {
            var entity = await _unitOfWork.Iitems.FindByIdAsync(id);
            if (entity is null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Item not found");
            entity.Name = item.Name;
            entity.Description = item.Description;
            entity.CollectionId = item.CollectionId;
            entity.LastUpdatedAt = TimeHelper.GetCurrentServerTime();
            _unitOfWork.Iitems.Update(id,entity);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }
    }
}
