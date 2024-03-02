using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Point { get; set; } = String.Empty;

        public NotFoundException(string point, string message)
            : base(message)
        {
            this.Point = point;
        }
    }
}
