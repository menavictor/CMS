using Asp.Versioning;
using CMS.Api.Extensions;
using CMS.Api.Extensions.Swagger.Headers;
using CMS.Api.Extensions.Swagger.Options;
using CMS.Application.Helper;
using CMS.Application.Mapping;
using CMS.Application.Services.Base;
using CMS.Application.Services.Identity.Permission;
using CMS.Common.Extensions;
using CMS.Common.Infrastructure.Repository.ActiveDirectory;
using CMS.Common.Infrastructure.UnitOfWork;
using CMS.Infrastructure.Context;
using CMS.Infrastructure.DataInitializer;
using CMS.Infrastructure.Repository.ActiveDirectory;
using CMS.Infrastructure.UnitOfWork;
using CMS.Integration.CacheRepository;
using CMS.Integration.FileRepository;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NetCore.AutoRegisterDi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

namespace CMS.Api.Extensions
{
    /// <summary>
    /// Dependency Extensions
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ConfigureDependencyExtension
    {
        private const string ConnectionStringName = "Default";
        /// <summary>
        /// Register Extensions
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddSignalR();
            services.RegisterDbContext(configuration);
            services.AddLocalizationServices();
            services.RegisterCores();
            services.RegisterRepository();
            services.RegisterIntegrationRepositories();
            services.RegisterAutoMapper();
            _ = services.RegisterCommonServices(configuration);
            services.RegisterApiMonitoring();
            _ = services.AddControllers();
            services.RegisterApiVersioning();
            services.RegisterSwaggerConfig();
            services.RegisterLowerCaseUrls();
            _ = services.AddControllersWithViews();
            return services;
        }

        /// <summary>
        /// Add DbContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddDbContext<CMSDbContext>(options =>
            {
                _ = options.UseSqlServer(configuration.GetConnectionString(ConnectionStringName));
            });
            _ = services.AddScoped<DbContext, CMSDbContext>();
            _ = services.AddSingleton<IDataInitializer, DataInitializer>();
        }

        /// <summary>
        /// Add Health Checks
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterApiMonitoring(this IServiceCollection services)
        {
            _ = services.AddHealthChecks()
                .AddDbContextCheck<CMSDbContext>();
        }


        /// <summary>
        /// register auto-mapper
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            _ = services.AddAutoMapper(typeof(MappingService));

        }

        /// <summary>
        /// Register localization
        /// </summary>
        /// <param name="services"></param>
        private static void AddLocalizationServices(this IServiceCollection services)
        {
            _ = services.AddLocalization();
        }

        /// <summary>
        /// Register Custom Repositories
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterRepository(this IServiceCollection services)
        {
            _ = services.AddScoped<IActiveDirectoryRepository, ActiveDirectoryRepository>();
        }

        /// <summary>
        /// register Integration Repositories
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterIntegrationRepositories(this IServiceCollection services)
        {
            _ = services.AddTransient<IFileRepository, FileRepository>();
            _ = services.AddTransient<ICacheRepository, CacheRepository>();
        }

        /// <summary>
        /// Register Api Versioning
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterApiVersioning(this IServiceCollection services)
        {
            _ = services.AddEndpointsApiExplorer();
            _ = services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            })
            .AddApiExplorer(config =>
            {
                config.GroupNameFormat = "'v'VVV";
                config.SubstituteApiVersionInUrl = true;
            });
        }


        /// <summary>
        /// Lower Case Urls
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterLowerCaseUrls(this IServiceCollection services)
        {
            _ = services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
        }

        /// <summary>
        /// Swagger Config
        /// </summary>
        /// <param name="services"></param>

        private static void RegisterSwaggerConfig(this IServiceCollection services)
        {
            _ = services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();
            _ = services.AddSwaggerGen(options =>
            {
                OpenApiSecurityRequirement security = new()
              {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] { }

                            }
                        };
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                options.AddSecurityRequirement(security);
                options.OperationFilter<LanguageHeader>();
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            _ = services.AddSwaggerGenNewtonsoftSupport();
        }

        /// <summary>
        /// Register Main Core
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterCores(this IServiceCollection services)
        {
            _ = services.AddSingleton<AppHelper>();
            _ = services.AddTransient(typeof(IBaseService<,,,,,>), typeof(BaseService<,,,,,>));
            _ = services.AddTransient(typeof(IServiceBaseParameter<>), typeof(ServiceBaseParameter<>));
            _ = services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            Assembly servicesToScan = Assembly.GetAssembly(typeof(PermissionService)); //..or whatever assembly you need
            _ = services.RegisterAssemblyPublicNonGenericClasses(servicesToScan)
                .Where(c => c.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();
        }

    }
}
