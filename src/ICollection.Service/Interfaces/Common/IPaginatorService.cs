using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Interfaces.Common
{
    public interface IPaginatorService
    {
        public Task<IList<T>> ToPagedAsync<T>(IList<T> items, int pageNumber, int pageSize);
    }
}
