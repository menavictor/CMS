using CMS.Application.Services.Base;
using CMS.Common.Core;
using CMS.Common.DTO.Base;
using CMS.Common.DTO.Business.GroupLevel;
using CMS.Domain.Entities.Business;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace CMS.Application.Services.Business.GroupLevel
{
    public class GroupLevelService(IServiceBaseParameter<Domain.Entities.Business.GroupLevel> parameters) : BaseService<Domain.Entities.Business.GroupLevel, AddGroupLevel, EditGroupLevel, GroupLevelDto, Guid, Guid?>(parameters), IGroupLevelService
    {

        #region Pulbic Methods
        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<IFinalResult> GetByIdAsync(object id)
        {
            Domain.Entities.Business.GroupLevel query = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == (Guid)id && !x.IsDeleted,
                include: a =>
                a.Include(b => b.EmployeeGroupLevels)

            );
            GroupLevelDto data = Mapper.Map<Domain.Entities.Business.GroupLevel, GroupLevelDto>(query);
            return ResponseResult.PostResult(result: data, status: HttpStatusCode.OK,
                message: "Data Retrieved Successfully");

        }

        public override async Task<IFinalResult> GetAllAsync(bool disableTracking = false, Expression<Func<Domain.Entities.Business.GroupLevel, bool>> predicate = null)
        {
            IEnumerable<Domain.Entities.Business.GroupLevel> query = await UnitOfWork.Repository.FindAsync(predicate: x => !x.IsDeleted,
                               include: a =>
                                              a.Include(b => b.EmployeeGroupLevels));
            IEnumerable<GroupLevelDto> data = Mapper.Map<IEnumerable<Domain.Entities.Business.GroupLevel>, IEnumerable<GroupLevelDto>>(query);


            return ResponseResult.PostResult(result: data, status: HttpStatusCode.OK,
                               message: "Data Retrieved Successfully");
        }

        /// <summary>
        /// Get All Pages
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<DataPaging> GetAllPagedAsync(BaseParam<GroupLevelFilter> filter)
        {
            int limit = filter.PageSize;
            int offset = --filter.PageNumber * filter.PageSize;
            (int, IEnumerable<Domain.Entities.Business.GroupLevel>) query = await UnitOfWork.Repository.FindPagedAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue
                , include: a =>
                a.Include(b => b.EmployeeGroupLevels));
            IEnumerable<GroupLevelDto> data = Mapper.Map<IEnumerable<Domain.Entities.Business.GroupLevel>, IEnumerable<GroupLevelDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, result: data, status: HttpStatusCode.OK, HttpStatusCode.OK.ToString());
        }
        /// <summary>
        /// Update Event
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task<IFinalResult> UpdateAsync(AddGroupLevel model)
        {
            IEnumerable<EmployeeGroupLevel> EmployeeGroupLevels = await UnitOfWork.GetRepository<EmployeeGroupLevel>()
                .FindAsync(a => a.GroupLevelId == model.Id);
            UnitOfWork.GetRepository<EmployeeGroupLevel>().RemoveRange(EmployeeGroupLevels);

            //await UnitOfWork.SaveChangesAsync();
            return await base.UpdateAsync(model);
        }


        #endregion

        #region Private Methods


        /// <summary>
        /// Predicate Builder
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<Domain.Entities.Business.GroupLevel, bool>> PredicateBuilderFunction(GroupLevelFilter filter)
        {
            ExpressionStarter<Domain.Entities.Business.GroupLevel> predicate = PredicateBuilder.New<Domain.Entities.Business.GroupLevel>(x => !x.IsDeleted);
            if (filter?.Name != null)
            {
                predicate = predicate.And(x => x.Name == filter.Name);
            }
            if (filter?.Description != null)
            {
                predicate = predicate.And(x => x.Description.Contains(filter.Description));
            }
            if (filter?.IsParent != null)
            {
                predicate = predicate.And(x => x.ParentId == null);
            }

            return predicate;
        }



        #endregion
    }
}
