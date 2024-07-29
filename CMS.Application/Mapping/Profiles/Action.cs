using CMS.Domain.Entities.Lookup;
using CMS.Common.DTO.Lookup.Action;


// ReSharper disable once CheckNamespace
namespace CMS.Application.Mapping
{
    public partial class MappingService
    {
        public void MapAction()
        {
            CreateMap<Action, ActionDto>()
                .ReverseMap();

            CreateMap<Action, AddActionDto>()
                .ReverseMap();

            CreateMap<Action, EditActionDto>()
                .ReverseMap();
        }
    }
}