using System.Reflection;
using InterviewApp.Configuration.Constants;
using InterviewApp.DependencyInjectionHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace InterviewApp
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataAccessDependencies(_configuration);
            services.AddBusinessLogicDependencies();
            services.AddApiClientsDependencies(_configuration);
            services.AddBackgroundTaskDependencies();
            services.AddPresentationLayerDependencies();

            var currentAppVersion = GetApplicationVersion();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(currentAppVersion,
                    new OpenApiInfo
                    {
                        Title = AppSettingsConstants.ProjectName,
                        Version = currentAppVersion
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                var currentAppVersion = GetApplicationVersion();
                var swaggerEndpointWithCurrentVersion =
                    string.Format(AppSettingsConstants.SwaggerEndpointUrl, currentAppVersion);

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint(swaggerEndpointWithCurrentVersion, AppSettingsConstants.ProjectName));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static string GetApplicationVersion()
        {
            var currentAppVersion = Assembly.GetEntryAssembly()!.GetName().Version!.ToString();
            return currentAppVersion;
        }
    }
}