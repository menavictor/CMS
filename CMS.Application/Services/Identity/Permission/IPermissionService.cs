using System.Threading.Tasks;
using CMS.Common.Core;
using CMS.Common.DTO.Base;
using CMS.Common.DTO.Identity.Permission;
using CMS.Common.DTO.Identity.Permission.Parameters;
using CMS.Application.Services.Base;

namespace CMS.Application.Services.Identity.Permission
{
    public interface IPermissionService : IBaseService<Domain.Entities.Identity.Permission, AddPermissionDto , EditPermissionDto, PermissionDto , int , int?>
    {
        Task<DataPaging> GetAllPagedAsync(BaseParam<PermissionFilter> filter);
    }
}