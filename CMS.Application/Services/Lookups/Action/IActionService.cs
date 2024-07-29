using System.Threading.Tasks;
using CMS.Common.Core;
using CMS.Common.DTO.Lookup.Action;
using CMS.Application.Services.Base;
using CMS.Common.DTO.Base;
using CMS.Common.DTO.Lookup.Action.Parameters;

namespace CMS.Application.Services.Lookups.Action
{
    public interface IActionService : IBaseService<CMS.Domain.Entities.Lookup.Action, AddActionDto , EditActionDto , ActionDto , int , int?>
    {
        Task<DataPaging> GetAllPagedAsync(BaseParam<ActionFilter> filter);
    }
}
