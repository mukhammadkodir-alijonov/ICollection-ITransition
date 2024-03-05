using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Interfaces.Common
{
    public interface IIdentityService
    {
        public int Id { get; }

        public string UserName { get; }

        public string ImagePath { get; }
    }
}
