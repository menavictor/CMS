using CMS.Domain.Entities.Lookup;
using CMS.Common.DTO.Lookup.Status;


// ReSharper disable once CheckNamespace
namespace CMS.Application.Mapping
{
    public partial class MappingService
    {
        public void MapStatus()
        {
            CreateMap<Status, AddStatusDto>()
                .ReverseMap();

            CreateMap<Status, StatusDto>()
                .ReverseMap();

            CreateMap<Status, EditStatusDto>()
                .ReverseMap();
        }
    }
}