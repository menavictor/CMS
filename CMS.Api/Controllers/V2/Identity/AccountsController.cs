using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CMS.Api.Controllers.V1.Base;
using CMS.Application.Services.Base;
using CMS.Application.Services.Identity.Account;
using CMS.Common.Core;
using CMS.Common.DTO.Identity.Account;

namespace CMS.Api.Controllers.V2.Identity
{
    /// <summary>
    /// Accounts Controller
    /// </summary>
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountsController : BaseController
    {
        private readonly IAccountService _service;
        /// <inheritdoc />
        public AccountsController(IAccountService accountService, ITokenService tokenService) : base(tokenService)
        {
            _service = accountService;
        }
        /// <summary>
        /// Login 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IFinalResult> Login(LoginParameters parameter)
        {
            return await _service.Login(parameter);
        }
    }
}
