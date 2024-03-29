﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.Dtos.Tags
{
    public class TagDto
    {
        public int TagId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ItemId { get; set; }
    }
}
