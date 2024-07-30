using CMS.Common.DTO.Business.GroupLevel;
using CMS.Domain.Entities.Business;


// ReSharper disable once CheckNamespace
namespace CMS.Application.Mapping
{
    public partial class MappingService
    {
        public void MapGroupLevel()
        {
            _ = CreateMap<GroupLevel, AddGroupLevel>()
                .ReverseMap();

            _ = CreateMap<GroupLevel, EditGroupLevel>()
                .ReverseMap();

            _ = CreateMap<GroupLevel, GroupLevelDto>()
                .ReverseMap();

            _ = CreateMap<EmployeeGroupLevel, EmployeeGroupLevelDto>()
               .ReverseMap();

            _ = CreateMap<EmployeeGroupLevel, AddEmployeeGroupLevel>()
               .ReverseMap();
        }
    }
}