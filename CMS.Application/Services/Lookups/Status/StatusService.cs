﻿using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CMS.Common.Core;
using CMS.Common.DTO.Lookup.Status;
using CMS.Application.Services.Base;
using LinqKit;
using System.Linq.Expressions;
using System;
using CMS.Common.DTO.Base;
using CMS.Common.DTO.Lookup.Status.Parameters;

namespace CMS.Application.Services.Lookups.Status
{
    public class StatusService : BaseService<CMS.Domain.Entities.Lookup.Status, AddStatusDto, StatusDto, EditStatusDto, int, int?>, IStatusService
    {
        
        public StatusService(IServiceBaseParameter<CMS.Domain.Entities.Lookup.Status> parameters) : base(parameters)
        {
        }

        public async Task<DataPaging> GetAllPagedAsync(BaseParam<StatusFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
            var data = Mapper.Map<IEnumerable<CMS.Domain.Entities.Lookup.Status>, IEnumerable<StatusDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, result: data, status: HttpStatusCode.OK, HttpStatusCode.OK.ToString());

        }

        static Expression<Func<CMS.Domain.Entities.Lookup.Status, bool>> PredicateBuilderFunction(StatusFilter filter)
        {
            var predicate = PredicateBuilder.New<CMS.Domain.Entities.Lookup.Status>(x => x.IsDeleted == filter.IsDeleted);

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
