using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Common.Validations
{
    public class PasswordValidator
    {
        public static (bool IsValid, string Message) IsStrong(string password)
        {
            bool isDigit = password.Any(x => char.IsDigit(x));
            if (!isDigit)
                return (IsValid: false, Message: "Password must contain at least 1 digit number.");

            return (IsValid: true, Message: "Valid password!");
        }
    }
}
