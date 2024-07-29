using System;
using System.Threading.Tasks;
using CMS.Common.Core;
using CMS.Common.DTO.Base;
using CMS.Common.DTO.Identity.User;
using CMS.Common.DTO.Identity.User.Parameters;
using CMS.Application.Services.Base;

namespace CMS.Application.Services.Identity.User
{
    public interface IUserService : IBaseService<Domain.Entities.Identity.User, AddUserDto , EditUserDto, UserDto , Guid, Guid?>
    {
        /// <summary>
        /// Get All Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging> GetAllPagedAsync(BaseParam<UserFilter> filter);
    }
}