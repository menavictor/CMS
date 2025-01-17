﻿using System;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using CMS.Api.Controllers.V1.Base;
using CMS.Application.Services.Identity.User;
using CMS.Common.Core;
using CMS.Common.DTO.Base;
using CMS.Common.DTO.Identity.User;
using CMS.Common.DTO.Identity.User.Parameters;

namespace CMS.Api.Controllers.V1.Identity
{
    /// <summary>
    /// Users Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : BaseController
    {
        private readonly IUserService _service;
        /// <summary>
        /// Constructor
        /// </summary>
        public UsersController(IUserService userService)
        {
            _service = userService;
        }
        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public async Task<IFinalResult> GetAsync(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result;
        }

        /// <summary>
        /// Get By Id For Edit 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getEdit/{id}")]
        public async Task<IFinalResult> GetEditAsync(Guid id)
        {
            var result = await _service.GetEditByIdAsync(id);
            return result;
        }

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        public async Task<IFinalResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return result;
        }

        /// <summary>
        /// Get Paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost("getPaged")]
        public async Task<DataPaging> GetPagedAsync([FromBody] BaseParam<UserFilter> filter)
        {
            return await _service.GetAllPagedAsync(filter);
        }

        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IFinalResult> AddAsync([FromBody] AddUserDto dto)
        {
            var result = await _service.AddAsync(dto);
            return result;
        }


        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IFinalResult> UpdateAsync(AddUserDto model)
        {
            return await _service.UpdateAsync(model);
        }
        /// <summary>
        /// Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IFinalResult> DeleteAsync(Guid id)
        {
            return await _service.DeleteAsync(id);
        }

        /// <summary>
        /// Soft Remove by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("deleteSoft/{id}")]
        public async Task<IFinalResult> DeleteSoftAsync(Guid id)
        {
            return await _service.DeleteSoftAsync(id);
        }


    }
}
