using CMS.Application.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers.V1.Base
{
    /// <inheritdoc />
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Token Business Property
        /// </summary>
        protected readonly ITokenService TokenService;
        /// <inheritdoc />
        public BaseController()
        {

        }
        /// <inheritdoc />
        public BaseController(ITokenService tokenService)
        {
            TokenService = tokenService;
        }
    }
}