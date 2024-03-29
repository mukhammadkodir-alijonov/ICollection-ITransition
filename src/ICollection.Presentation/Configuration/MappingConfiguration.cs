using AutoMapper;
using ICollection.Domain.Entities.Admins;
using ICollection.Domain.Entities.Items;
using ICollection.Domain.Entities.Users;
using ICollection.Service.Dtos.Accounts;
using ICollection.Service.Dtos.Admins;
using ICollection.Service.ViewModels.ItemViewModels;

namespace ICollection.Presentation.Configuration
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<AdminRegisterDto, Admin>().ReverseMap();
            CreateMap<AccountRegisterDto, User>().ReverseMap();
            CreateMap<ItemViewModel,Item>().ReverseMap();
        }
    }
}
