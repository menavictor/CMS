﻿using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using CMS.Application.Services.Base;
using CMS.Common.Core;
using CMS.Common.DTO.Base;
using CMS.Common.DTO.Lookup.Category;
using CMS.Common.DTO.Lookup.Category.Parameters;

namespace CMS.Application.Services.Lookups.Category
{
    public class CategoryService : BaseService<Domain.Entities.Lookup.Category, AddCategoryDto, EditCategoryDto , CategoryDto, int, int?>, ICategoryService
    {
        
        public CategoryService(IServiceBaseParameter<Domain.Entities.Lookup.Category> parameters) : base(parameters)
        {
        }

        public async Task<DataPaging> GetAllPagedAsync(BaseParam<CategoryFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
            var data = Mapper.Map<IEnumerable<Domain.Entities.Lookup.Category>, IEnumerable<CategoryDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, result: data, status: HttpStatusCode.OK, HttpStatusCode.OK.ToString());

        }

        static Expression<Func<Domain.Entities.Lookup.Category, bool>> PredicateBuilderFunction(CategoryFilter filter)
        {
            var predicate = PredicateBuilder.New<Domain.Entities.Lookup.Category>(x => x.IsDeleted == filter.IsDeleted);

            if (!string.IsNullOrWhiteSpace(filter?.NameAr))
            {
                predicate = predicate.And(b => b.NameAr.ToLower().Contains(filter.NameAr.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter?.NameEn))
            {
                predicate = predicate.And(b => b.NameEn.ToLower().Contains(filter.NameEn.ToLower()));
            }
            return predicate;
        }

    }
}
