using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.ViewModels.ItemViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string ImagePath { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public Dictionary<string, object>? CustomFieldValues { get; set; }
        public DateTime CreatedAt { get; set; } = default!;
    }
}
