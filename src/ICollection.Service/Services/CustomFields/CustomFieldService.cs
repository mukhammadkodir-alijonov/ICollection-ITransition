using ICollection.DataAccess.Interfaces.Common;
using ICollection.Domain.Entities.CustomFields;
using ICollection.Service.Common.Exceptions;
using ICollection.Service.Dtos.CustomFields;
using ICollection.Service.Interfaces.CustomFields;

namespace ICollection.Service.Services.CustomFields
{
    public class CustomFieldService : ICustomFieldService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomFieldService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateCustomFieldAsync(int id, CustomFieldDto customFieldDto)
        {
            var field = await _unitOfWork.CustomFields.FirstOrDefault(x => x.Id == id);
            if (field == null)
            {
                var entity = new CustomField
                {
                    Name = customFieldDto.Name,
                    Type = customFieldDto.Type
                };
                var res = _unitOfWork.CustomFields.Add(entity);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            else throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "CustomFieldCollection is not created or already exists");
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
