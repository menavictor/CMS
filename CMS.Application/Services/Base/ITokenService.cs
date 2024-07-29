using CMS.Common.DTO.Identity.Account;
using CMS.Common.DTO.Identity.User;
using CMS.Domain.Entities.Identity;

namespace CMS.Application.Services.Base
{
    public interface ITokenService
    {
        UserLoginReturn GenerateJsonWebToken(UserDto userInfo, Role role);
    }
}