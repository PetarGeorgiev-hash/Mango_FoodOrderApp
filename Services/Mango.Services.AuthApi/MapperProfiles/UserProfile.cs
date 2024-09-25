using AutoMapper;
using Mango.Services.AuthApi.Dto;
using Mango.Services.AuthApi.Models;

namespace Mango.Services.AuthApi.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UserDto, ApplicationUser>();
            CreateMap<RegisterDto,ApplicationUser>().ForMember(x => x.UserName , opt => opt.MapFrom(src => src.Name));
            CreateMap<ApplicationUser, RegisterDto>().ForMember(x => x.Name, opt => opt.MapFrom(src => src.UserName));
            CreateMap<UserDto, RegisterDto>();
            CreateMap<RegisterDto, UserDto>();

        }
    }
}
