using Asp.Versioning.ApiExplorer;
using CMS.Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CMS.Api.Extensions
{
    /// <summary>
    /// Pipeline Extensions
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ConfigureMiddlewareExtension
    {
        /// <summary>
        /// General Configuration Method
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="configuration"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IApplicationBuilder Configure(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration, IApiVersionDescriptionProvider provider)
        {
            app.ConfigureCors();
            app.CreateDatabase();
            _ = app.UseHttpsRedirection();
            _ = app.UseRouting();
            _ = app.UseAuthentication();
            _ = app.UseAuthorization();
            app.AddLocalization();
            app.UseFluentScheduler(configuration);
            app.SwaggerConfig(provider);
            _ = app.UseHealthChecks("/probe");
            return app;
        }
        /// <summary>
        /// Configure Cors
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureCors(this IApplicationBuilder app)
        {
            _ = app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        }

        /// <summary>
        /// Create Database From Migration
        /// </summary>
        /// <param name="app"></param>
        public static void CreateDatabase(this IApplicationBuilder app)
        {
            try
            {
                using IServiceScope scope =
                    app.ApplicationServices.CreateScope();
                using CMSDbContext context = scope.ServiceProvider.GetService<CMSDbContext>();
                context?.Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        /// <summary>
        /// Add Localization
        /// </summary>
        /// <param name="app"></param>
        public static void AddLocalization(this IApplicationBuilder app)
        {
            string[] supportedCultures = new[] { "en-US", "ar-OM" };
            RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures()
                .AddSupportedUICultures();
            _ = app.UseRequestLocalization(localizationOptions);
        }

        /// <summary>
        /// User Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <param name="provider"></param>
        private static void SwaggerConfig(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI(options =>
            {
                foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant()
                    );
                }
                options.DocExpansion(DocExpansion.List);
            });
        }

        /// <summary>
        /// User Fluent Scheduler
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void UseFluentScheduler(this IApplicationBuilder app, IConfiguration configuration)
        {
            _ = configuration["Environment"];
            //if (env == Environment.Development)
            //{
            //    JobManager.Initialize(new MyRegistry());
            //}

        }
    }
}
