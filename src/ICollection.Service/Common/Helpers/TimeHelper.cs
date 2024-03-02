using ICollection.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Common.Helpers
{
    public class TimeHelper
    {
        public static DateTime GetCurrentServerTime()
        {
            var date = DateTime.UtcNow;
            return date.AddHours(TimeConstants.UTC);
        }
    }
}
