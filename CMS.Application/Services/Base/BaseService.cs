using AutoMapper;
using CMS.Common.Core;
using CMS.Common.DTO.Base;
using CMS.Common.Infrastructure.Repository;
using CMS.Common.Infrastructure.UnitOfWork;
using CMS.Domain.Enum;
using CMS.Integration.CacheRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.Enum;
namespace CMS.Application.Services.Base
{

    public class BaseService<T>
       where T : class
    {
        protected readonly IUnitOfWork<T> UnitOfWork;
        protected readonly IRepository<T> Repository;
        protected readonly IMapper Mapper;
        protected readonly IResponseResult ResponseResult;
        protected IFinalResult Result;
        protected IHttpContextAccessor HttpContextAccessor;
        protected IConfiguration Configuration;
        protected ICacheRepository CacheRepository;
        protected TokenClaimDto ClaimData { get; set; }

        protected internal BaseService(IServiceBaseParameter<T> businessBaseParameter)
        {
            HttpContextAccessor = businessBaseParameter.HttpContextAccessor;
            UnitOfWork = businessBaseParameter.UnitOfWork;
            Repository = businessBaseParameter.UnitOfWork.Repository;
            ResponseResult = businessBaseParameter.ResponseResult;
            Mapper = businessBaseParameter.Mapper;
            CacheRepository = businessBaseParameter.CacheRepository;
            Configuration = businessBaseParameter.Configuration;
            ClaimsPrincipal claims = HttpContextAccessor?.HttpContext?.User;
            ClaimData = GetTokenClaimDto(claims);

        }
        /// <summary>
        /// Get Claims From Token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        private TokenClaimDto GetTokenClaimDto(ClaimsPrincipal claims)
        {
            _ = TryParse(claims?.FindFirst(t => t.Type == "UserType")?.Value ?? "1", out UserType userType);
            TokenClaimDto claimData = new()
            {
                UserId = claims?.FindFirst(t => t.Type == "UserId")?.Value,

                Email = claims?.FindFirst(t => t.Type == "Email")?.Value,


                UserType = userType
            };
            if (!string.IsNullOrWhiteSpace(claims?.FindFirst(t => t.Type == "UserTypeId")?.Value))
            {
                claimData.UserTypeId = long.Parse(claims.FindFirst(t => t.Type == "UserTypeId")?.Value ?? "0");
            }
            return claimData;
        }

        public IFinalResult Success(object result, HttpStatusCode status = HttpStatusCode.OK, Exception exception = null, string message = "AddSuccess")

        {
            return ResponseResult.PostResult(result, status: HttpStatusCode.OK,
                message: "SUCCESS");
        }


    }
    public class BaseService<T, TAddDto, TEditDto, TGetDto, TKey, TKeyDto>
          : IBaseService<T, TAddDto, TEditDto, TGetDto, TKey, TKeyDto>
          where T : class
          where TAddDto : IEntityDto<TKeyDto>
          where TEditDto : IEntityDto<TKeyDto>
          where TGetDto : IEntityDto<TKeyDto>
    {
        protected readonly IRepository<T> Repository;
        protected readonly IUnitOfWork<T> UnitOfWork;
        protected readonly IMapper Mapper;
        protected readonly IResponseResult ResponseResult;
        protected IFinalResult Result;
        protected IHttpContextAccessor HttpContextAccessor;
        protected IConfiguration Configuration;
        protected ICacheRepository CacheRepository;
        protected TokenClaimDto ClaimData { get; set; }

        protected internal BaseService(IServiceBaseParameter<T> businessBaseParameter)
        {
            HttpContextAccessor = businessBaseParameter.HttpContextAccessor;
            UnitOfWork = businessBaseParameter.UnitOfWork;
            ResponseResult = businessBaseParameter.ResponseResult;
            Mapper = businessBaseParameter.Mapper;
            CacheRepository = businessBaseParameter.CacheRepository;
            Configuration = businessBaseParameter.Configuration;
            ClaimsPrincipal claims = HttpContextAccessor?.HttpContext?.User;
            ClaimData = GetTokenClaimDto(claims);
            Repository = businessBaseParameter.UnitOfWork.Repository;
        }

        public virtual async Task<IFinalResult> GetByIdAsync(object id)
        {
            T query = await UnitOfWork.Repository.GetAsync(id);
            TGetDto data = Mapper.Map<T, TGetDto>(query);
            return ResponseResult.PostResult(result: data, status: HttpStatusCode.OK,
                message: "SUCCESS");
        }

        public virtual async Task<IFinalResult> GetEditByIdAsync(object id)
        {
            T query = await UnitOfWork.Repository.GetAsync(id);
            TEditDto data = Mapper.Map<T, TEditDto>(query);
            return ResponseResult.PostResult(result: data, status: HttpStatusCode.OK,
                message: "SUCCESS");
        }

        public virtual async Task<IFinalResult> GetAllAsync(bool disableTracking = false, Expression<Func<T, bool>> predicate = null)
        {
            IEnumerable<T> query = predicate != null
              ? await UnitOfWork.Repository.FindAsync(predicate)
              : await UnitOfWork.Repository.GetAllAsync(disableTracking: disableTracking);
            IEnumerable<TGetDto> data = Mapper.Map<IEnumerable<T>, IEnumerable<TGetDto>>(query);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());
        }

        public virtual async Task<IFinalResult> AddAsync(TAddDto model)
        {
            T entity = Mapper.Map<TAddDto, T>(model);
            SetEntityCreatedBaseProperties(entity);
            _ = UnitOfWork.Repository.Add(entity);
            int affectedRows = await UnitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                Result = new ResponseResult(result: null, status: HttpStatusCode.Created,
                    message: "ADD_SUCCESS");
            }
            Result.Data = model;
            return Result;
        }

        public virtual async Task<IFinalResult> AddListAsync(List<TAddDto> model)
        {
            List<T> entities = Mapper.Map<List<TAddDto>, List<T>>(model);
            UnitOfWork.Repository.AddRange(entities);
            int affectedRows = await UnitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                Result = new ResponseResult(result: null, status: HttpStatusCode.Created,
                    message: "ADD_SUCCESS");
            }
            Result.Data = model;
            return Result;
        }

        public virtual async Task<IFinalResult> UpdateAsync(TAddDto model)
        {
            T entityToUpdate = await UnitOfWork.Repository.GetAsync(model.Id);
            T newEntity = Mapper.Map(model, entityToUpdate);
            SetEntityModifiedBaseProperties(newEntity);
            UnitOfWork.Repository.Update(entityToUpdate, newEntity);
            int affectedRows = await UnitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.Accepted,
                    message: "UPDATE_SUCCESS");
            }
            return Result;

        }

        public virtual async Task<IFinalResult> DeleteAsync(object id)
        {
            T entityToDelete = await UnitOfWork.Repository.GetAsync(id);
            UnitOfWork.Repository.Remove(entityToDelete);
            int affectedRows = await UnitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.Accepted,
                    message: "DELETE_SUCCESS");
            }
            return Result;
        }

        public virtual async Task<IFinalResult> DeleteSoftAsync(object id)
        {
            T entityToDelete = await UnitOfWork.Repository.GetAsync(id);
            SetEntityModifiedBaseProperties(entityToDelete);
            UnitOfWork.Repository.RemoveLogical(entityToDelete);
            int affectedRows = await UnitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.Accepted,
                    message: "DELETE_SUCCESS");
            }
            return Result;
        }

        protected void SetEntityCreatedBaseProperties(T entity)
        {
            Type type = entity.GetType();
            System.Reflection.PropertyInfo createdBy = type.GetProperty("CreatedByUserId");
            createdBy?.SetValue(entity, new Guid(ClaimData.UserId));
            System.Reflection.PropertyInfo createdDate = type.GetProperty("CreatedDate");
            createdDate?.SetValue(entity, DateTime.UtcNow);

        }

        protected void SetEntityModifiedBaseProperties(T entity)
        {
            Type type = entity.GetType();
            System.Reflection.PropertyInfo createdBy = type.GetProperty("ModifiedByUserId");
            createdBy?.SetValue(entity, new Guid(ClaimData.UserId));
            System.Reflection.PropertyInfo createdDate = type.GetProperty("ModifiedDate");
            createdDate?.SetValue(entity, DateTime.UtcNow);

        }

        public IFinalResult Success(object result, HttpStatusCode status = HttpStatusCode.OK, Exception exception = null, string message = "AddSuccess")
        {
            return ResponseResult.PostResult(result, status: HttpStatusCode.OK,
                message: "SUCCESS");
        }
        private TokenClaimDto GetTokenClaimDto(ClaimsPrincipal claims)
        {
            _ = TryParse(claims?.FindFirst(t => t.Type == "UserType")?.Value ?? "1", out UserType userType);
            TokenClaimDto claimData = new()
            {
                UserId = claims?.FindFirst(t => t.Type == "UserId")?.Value,

                Email = claims?.FindFirst(t => t.Type == "Email")?.Value,


                UserType = userType
            };
            if (!string.IsNullOrWhiteSpace(claims?.FindFirst(t => t.Type == "UserTypeId")?.Value))
            {
                claimData.UserTypeId = long.Parse(claims.FindFirst(t => t.Type == "UserTypeId")?.Value ?? "0");
            }
            return claimData;
        }

    }



}
