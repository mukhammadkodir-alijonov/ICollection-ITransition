﻿using ICollection.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Domain.Entities
{
    public class Human : Auditable
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Image { get; set; }
        public DateTime BirthDate { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
    }
}
