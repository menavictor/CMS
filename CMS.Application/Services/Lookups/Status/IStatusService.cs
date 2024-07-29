using System.Threading.Tasks;
using CMS.Common.Core;
using CMS.Common.DTO.Lookup.Status;
using CMS.Application.Services.Base;
using CMS.Common.DTO.Base;
using CMS.Common.DTO.Lookup.Status.Parameters;

namespace CMS.Application.Services.Lookups.Status
{
    public interface IStatusService : IBaseService<CMS.Domain.Entities.Lookup.Status , AddStatusDto ,EditStatusDto, StatusDto , int , int?>
    {
        Task<DataPaging> GetAllPagedAsync(BaseParam<StatusFilter> filter);
    }
}
