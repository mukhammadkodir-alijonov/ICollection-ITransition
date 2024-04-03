using AutoMapper;
using ICollection.Domain.Entities.Admins;
using ICollection.Domain.Entities.Comments;
using ICollection.Domain.Entities.Items;
using ICollection.Domain.Entities.Users;
using ICollection.Service.Dtos.Accounts;
using ICollection.Service.Dtos.Admins;
using ICollection.Service.ViewModels.CommentViewModels;
using ICollection.Service.ViewModels.ItemViewModels;
using ICollection.Service.ViewModels.UserViewModels;

namespace ICollection.Presentation.Configuration
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<AdminRegisterDto, Admin>().ReverseMap();
            CreateMap<AccountRegisterDto, User>().ReverseMap();
            CreateMap<ItemViewModel,Item>().ReverseMap();
            CreateMap<UserViewModel,User>().ReverseMap();
            CreateMap<CommentViewModel,Comment>().ReverseMap();
        }
    }
}
