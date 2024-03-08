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
        public Task<bool> CreateCustomFieldAsync(CustomFieldDto customField)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCustomFieldAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
