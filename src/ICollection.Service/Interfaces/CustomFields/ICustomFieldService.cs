using ICollection.Service.Dtos.CustomFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Interfaces.CustomFields
{
    public interface ICustomFieldService
    {
        public Task<bool> CreateCustomFieldForCollectionAsync(CustomFieldDto customField);
        public Task<bool> CreateCustomFieldForItemAsync(CustomFieldDto customField);
        public Task<bool> DeleteCustomFieldAsync(int id);
    }
}
