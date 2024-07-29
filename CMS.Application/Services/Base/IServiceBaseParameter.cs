using AutoMapper;
using CMS.Common.Core;
using CMS.Common.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using CMS.Integration.CacheRepository;
using Microsoft.Extensions.Logging;

namespace CMS.Application.Services.Base
{
    public interface IServiceBaseParameter<T> where T : class
    {
        IMapper Mapper { get; set; }

        IUnitOfWork<T> UnitOfWork { get; set; }

        IResponseResult ResponseResult { get; set; }

        IHttpContextAccessor HttpContextAccessor { get; set; }

        IConfiguration Configuration { get; set; }

        ICacheRepository CacheRepository { get; set; }

        ILogger Logger { get; set; }
    }
}