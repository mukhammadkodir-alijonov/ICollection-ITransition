using ICollection.Service.Dtos.CustomFields;

namespace ICollection.Service.Interfaces.CustomFields
{
    public interface ICustomFieldService
    {
        public Task<bool> CreateCustomFieldAsync(int id, CustomFieldDto customField);
        public Task<bool> DeleteCustomFieldAsync(int id);
    }
}
