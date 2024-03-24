using AutoMapper;
using ICollection.Domain.Entities.Admins;
using ICollection.Domain.Entities.Users;
using ICollection.Service.Dtos.Accounts;
using ICollection.Service.Dtos.Admins;

namespace ICollection.Presentation.Configuration
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<AdminRegisterDto, Admin>().ReverseMap();
            CreateMap<AccountRegisterDto, User>().ReverseMap();
        }
    }
}
