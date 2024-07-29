using Asp.Versioning.ApiExplorer;
using CMS.Api.Extensions;
using CMS.Api.MiddleWares;
using CMS.Application.DependencyExtension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CMS.Api
{
    /// <summary>
    /// Start Up Class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly Shell _shell;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _shell = (Shell)Activator.CreateInstance(typeof(Shell));
        }
        /// <summary>
        /// Public Configuration Property
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure Dependencies
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.RegisterServices(Configuration);

        }

        /// <summary>
        /// Configure Pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            _shell.ConfigureHttp(app, env);
            Shell.Start(_shell);

            _ = app.Configure(env, Configuration, provider);
            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomMiddleware();


            _ = app.UseEndpoints(endpoints =>
            {
                _ = endpoints.MapControllers();
                _ = endpoints.MapDefaultControllerRoute();

            });
        }
    }
}