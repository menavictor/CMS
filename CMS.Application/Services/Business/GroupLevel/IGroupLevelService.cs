using CMS.Application.Services.Base;
using CMS.Common.Core;
using CMS.Common.DTO.Base;
using CMS.Common.DTO.Business.GroupLevel;
using System;
using System.Threading.Tasks;

namespace CMS.Application.Services.Business.GroupLevel
{
    public interface IGroupLevelService : IBaseService<Domain.Entities.Business.GroupLevel, AddGroupLevel, EditGroupLevel, GroupLevelDto, Guid, Guid?>
    {
        /// <summary>
        /// Get All Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging> GetAllPagedAsync(BaseParam<GroupLevelFilter> filter);

    }
}
