using AutoMapper;
using Store.BusinessMS.Users.Application.Dtos;
using Store.BusinessMS.Users.Domain;
using Store.BusinessMS.Users.Domain.User;

namespace Store.BusinessMS.Users.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, GetByIdDto>().ReverseMap();
            CreateMap<GetByIdDto, ApplicationUser>().ReverseMap();
            CreateMap<ApplicationUser, UserDto>();
        }
    }
}
