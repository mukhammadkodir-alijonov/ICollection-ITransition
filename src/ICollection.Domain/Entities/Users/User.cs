using ICollection.Domain.Enums;

namespace ICollection.Domain.Entities.Users
{
    public class User : Person
    {
        public Role UserRole { get; set; } = Role.User;
        public StatusType Status { get; set; } = StatusType.Active;
    }
}
