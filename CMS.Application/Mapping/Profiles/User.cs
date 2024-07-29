using CMS.Common.DTO.Identity.User;
using CMS.Domain.Entities.Identity;

// ReSharper disable once CheckNamespace
namespace CMS.Application.Mapping
{
    public partial class MappingService
    {
        public void MapUser()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();

            CreateMap<User, AddUserDto>()
                .ReverseMap();

            CreateMap<User, EditUserDto>()
                .ReverseMap();

            CreateMap<User, ActiveDirectoryUserDto>()
                .ReverseMap();
            CreateMap<ActiveDirectoryUserDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.LogonName))
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Principal))
                .ReverseMap();
        }
    }
}