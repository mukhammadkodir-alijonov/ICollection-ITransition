using ICollection.Domain.Entities.Collections;
using ICollection.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICollection.Service.ViewModels.CollectionViewModels
{
    public class CollectionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;
        public Topics Topics { get; set; } = Topics.Other;
        //public SelectList? Topics { get; set; } = new SelectList(Enum.GetValues(typeof(Topics)));

        public string ImagePath { get; set; } = String.Empty;

        public DateTime CreatedAt { get; set; } = default!;
        public DateTime LastUpdatedAt { get; set; } = default!;
        public int LikeCount { get; set; } = default!;
        public int UserId { get; set; }
        public int CustomFieldId { get; set; }

        public static implicit operator CollectionViewModel(Collection model)
        {
            return new CollectionViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                ImagePath = model.Image,
                Topics = model.Topics,
                CreatedAt = model.CreatedAt
            };
        }
    }
}
