using ICollection.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.ViewModels.CustomFieldViewModels
{
    public class CustomFieldViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;
        public FieldType Type { get; set; } = FieldType.String;
    }
}
