using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.ViewModels
{
    public class AccountBaseViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; } = String.Empty;

        public string ImagePath { get; set; } = String.Empty;
    }
}
