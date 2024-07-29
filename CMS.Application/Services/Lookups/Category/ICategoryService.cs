using System.Threading.Tasks;
using CMS.Application.Services.Base;
using CMS.Common.Core;
using CMS.Common.DTO.Base;
using CMS.Common.DTO.Lookup.Category;
using CMS.Common.DTO.Lookup.Category.Parameters;

namespace CMS.Application.Services.Lookups.Category
{
    public interface ICategoryService : IBaseService<Domain.Entities.Lookup.Category, AddCategoryDto , EditCategoryDto , CategoryDto , int , int?>
    {
        Task<DataPaging> GetAllPagedAsync(BaseParam<CategoryFilter> filter);
    }
}
