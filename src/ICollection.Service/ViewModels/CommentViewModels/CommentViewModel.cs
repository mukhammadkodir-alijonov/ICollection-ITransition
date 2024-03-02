using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.ViewModels.CommentViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; } = String.Empty;
    }
}
