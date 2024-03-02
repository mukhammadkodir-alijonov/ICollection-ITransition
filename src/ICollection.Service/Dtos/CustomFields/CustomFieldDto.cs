using ICollection.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Dtos.CustomFields
{
    public class CustomFieldDto
    {
        public string Name { get; set; } = string.Empty;
        public FieldType Type { get; set; } = FieldType.String;
    }
}
