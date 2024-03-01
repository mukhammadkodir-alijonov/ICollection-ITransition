﻿using ICollection.Domain.Common;

namespace ICollection.Domain.Entities
{
    public class Person : Auditable
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
    }
}
