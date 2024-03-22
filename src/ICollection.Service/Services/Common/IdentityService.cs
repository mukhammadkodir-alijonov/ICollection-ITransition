using ICollection.Service.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Services.Common
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _accessor;
        public IdentityService(IHttpContextAccessor accessor)
        {
            this._accessor = accessor;
        }

        public int? Id
        {
            get
            {
                var res = _accessor.HttpContext!.User.FindFirst("Id");
                return res is not null ? int.Parse(res.Value) : null;
            }
        }

        public string UserName
        {
            get
            {
                var result = _accessor.HttpContext!.User.FindFirst("UserName");
                return (result is null) ? String.Empty : result.Value;
            }
        }

        public string ImagePath
        {
            get
            {
                var result = _accessor.HttpContext!.User.FindFirst("Image");
                return (result is null) ? String.Empty : result.Value;
            }
        }
    }
}
