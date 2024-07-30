using Asp.Versioning;
using CMS.Api.Controllers.V1.Base;
using CMS.Application.Services.Business.GroupLevel;
using CMS.Common.Core;
using CMS.Common.DTO.Base;
using CMS.Common.DTO.Business.GroupLevel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CMS.Api.Controllers.V1.Business
{
    /// <summary>
    /// Packages Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [AllowAnonymous]
    public class GroupLevelsController : BaseController
    {
        private readonly IGroupLevelService _groupLevelService;
        /// <summary>
        /// Constructor
        /// </summary>
        public GroupLevelsController(IGroupLevelService GroupLevelService)
        {
            _groupLevelService = GroupLevelService;
        }
        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-Id/{id}")]
        public async Task<IFinalResult> GetAsync(Guid id)
        {
            IFinalResult result = await _groupLevelService.GetByIdAsync(id);
            return result;
        }



        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        public async Task<IFinalResult> GetAllAsync()
        {
            IFinalResult result = await _groupLevelService.GetAllAsync();
            return result;
        }

        /// <summary>
        /// GetAll Data paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost("getPaged")]
        public async Task<DataPaging> GetPagedAsync([FromBody] BaseParam<GroupLevelFilter> filter)
        {
            return await _groupLevelService.GetAllPagedAsync(filter);
        }
        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IFinalResult> AddAsync([FromBody] AddGroupLevel dto)
        {
            IFinalResult result = await _groupLevelService.AddAsync(dto);
            return result;
        }

        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IFinalResult> UpdateAsync(AddGroupLevel model)
        {

            return await _groupLevelService.UpdateAsync(model);
        }

        /// <summary>
        /// Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IFinalResult> DeleteAsync(Guid id)
        {
            return await _groupLevelService.DeleteAsync(id);
        }
        /// <summary>
        /// logical delete by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("deleteSoft/{id}")]
        public async Task<IFinalResult> DeleteSoftAsync(Guid id)
        {
            return await _groupLevelService.DeleteSoftAsync(id);
        }
    }
}
