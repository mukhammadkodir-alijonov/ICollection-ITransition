using ICollection.Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Common
{
    public class PaginatorService : IPaginatorService
    {
        public Task<IList<T>> ToPagedAsync<T>(IList<T> items, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
