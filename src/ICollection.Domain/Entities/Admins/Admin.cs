using ICollection.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Domain.Entities.Admins
{
    public class Admin : Person
    {
        public string Address { get; set; } = String.Empty;
        public Role AdminRole { get; set; } = Role.Admin;
        public StatusType Status { get; set; } = StatusType.Active;
    }
}
