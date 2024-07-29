using CMS.Common.DTO.Identity.Permission;
using CMS.Domain.Entities.Identity;


// ReSharper disable once CheckNamespace
namespace CMS.Application.Mapping
{
    public partial class MappingService
    {
        public void MapPermission()
        {
            CreateMap<Permission, PermissionDto>()
                .ReverseMap();

            CreateMap<Permission, AddPermissionDto>()
                .ReverseMap();

            CreateMap<Permission, EditPermissionDto>()
                .ReverseMap();
        }
    }
}