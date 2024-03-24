using ICollection.DataAccess.Interfaces.Common;
using ICollection.Domain.Entities.CustomFields;
using ICollection.Service.Common.Exceptions;
using ICollection.Service.Dtos.CustomFields;
using ICollection.Service.Interfaces.CustomFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.CustomFields
{
    public class CustomFieldService : ICustomFieldService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomFieldService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateCustomFieldForCollectionAsync(CustomFieldDto customFieldDto)
        {
            var field = await _unitOfWork.CustomFields.FirstOrDefault(x => x.Id == customFieldDto.CollectionId);
            if (field == null)
            {
                var entity = new CustomField
                {
                    Name = customFieldDto.Name,
                    Type = customFieldDto.Type,
                    CollectionId = customFieldDto.CollectionId
                };
                var res = _unitOfWork.CustomFields.Add(entity);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            else throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "CustomFieldCollection is not created or already exists");
        }

        public async Task<bool> CreateCustomFieldForItemAsync(CustomFieldDto customFieldDto)
        {
            var field = await _unitOfWork.CustomFields.FirstOrDefault(x => x.Id == customFieldDto.ItemId);
            if (field == null)
            {
                var entity = new CustomField
                {
                    Name = customFieldDto.Name,
                    Type = customFieldDto.Type,
                    ItemId = customFieldDto.ItemId
                };
                var res = _unitOfWork.CustomFields.Add(entity);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            else throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "CustomFieldItem is not created or already exists");
        }

        public async Task<bool> DeleteCustomFieldAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid custom field ID");
            }

            var customFieldToDelete = await _unitOfWork.CustomFields.FindByIdAsync(id);

            if (customFieldToDelete == null)
            {
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Custom field not found");
            }

            _unitOfWork.CustomFields.Delete(id); // Pass the ID of the custom field to delete
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }
    }
}
