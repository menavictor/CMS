﻿using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using CMS.Api.Controllers.V1.Base;
using CMS.Application.Services.Lookups.Category;
using CMS.Common.Core;
using CMS.Common.DTO.Base;
using CMS.Common.DTO.Lookup.Category;
using CMS.Common.DTO.Lookup.Category.Parameters;

namespace CMS.Api.Controllers.V2.Lookup
{
    /// <summary>
    /// Categories Controller
    /// </summary>
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;
        /// <summary>
        /// Constructor
        /// </summary>
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public async Task<IFinalResult> GetAsync(int id) => await _categoryService.GetByIdAsync(id);


        /// <summary>
        /// Get By Id For Edit 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getEdit/{id}")]
        public async Task<IFinalResult> GetEditAsync(int id) => await _categoryService.GetEditByIdAsync(id);


        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        public async Task<IFinalResult> GetAllAsync() => await _categoryService.GetAllAsync();


        /// <summary>
        /// Get Paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost("getPaged")]
        public async Task<DataPaging> GetPagedAsync([FromBody] BaseParam<CategoryFilter> filter) => await _categoryService.GetAllPagedAsync(filter);


        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IFinalResult> AddAsync([FromBody] AddCategoryDto dto) => await _categoryService.AddAsync(dto);


        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IFinalResult> UpdateAsync(AddCategoryDto model) => await _categoryService.UpdateAsync(model);

        /// <summary>
        /// Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IFinalResult> DeleteAsync(int id) => await _categoryService.DeleteAsync(id);


        /// <summary>
        /// Soft Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("deleteSoft/{id}")]
        public async Task<IFinalResult> DeleteSoftAsync(int id) => await _categoryService.DeleteSoftAsync(id);

    }
}
