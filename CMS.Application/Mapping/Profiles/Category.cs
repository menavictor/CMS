using CMS.Common.DTO.Lookup.Category;
using CMS.Domain.Entities.Lookup;


// ReSharper disable once CheckNamespace
namespace CMS.Application.Mapping
{
    public partial class MappingService
    {
        public void MapCategory()
        {
            CreateMap<Category, CategoryDto>()
                .ReverseMap();

            CreateMap<Category, AddCategoryDto>()
                .ReverseMap();

            CreateMap<Category, EditCategoryDto>()
                .ReverseMap();
        }
    }
}