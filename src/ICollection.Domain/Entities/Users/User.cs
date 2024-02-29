using ICollection.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Domain.Entities.Users
{
    public class User : Person
    {
        public Role UserRole { get; set; } = Role.User;
        public StatusType Status { get; set; } = StatusType.Active;
    }
}
