using ICollection.Domain.Enums;

namespace ICollection.Domain.Entities.Admins
{
    public class Admin : Person
    {
        public string Address { get; set; } = String.Empty;
        public Role AdminRole { get; set; } = Role.Admin;
        public StatusType Status { get; set; } = StatusType.Active;
    }
}
