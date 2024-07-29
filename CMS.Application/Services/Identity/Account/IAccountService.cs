using System.Threading.Tasks;
using CMS.Common.Core;
using CMS.Common.DTO.Identity.Account;

namespace CMS.Application.Services.Identity.Account
{
    public interface IAccountService
    {
        Task<IFinalResult> Login(LoginParameters parameters);
        Task<IFinalResult> AdLogin(LoginParameters parameters);
    }
}