using CMS.Common.DTO.Identity.Account;
using CMS.Common.DTO.Identity.User;

namespace CMS.Common.Infrastructure.Repository.ActiveDirectory
{
    public interface IActiveDirectoryRepository
    {
        ActiveDirectoryUserDto LoginAsync(LoginParameters parameters);
    }
}
