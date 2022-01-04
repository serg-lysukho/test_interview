using System;
using System.Net.Http;
using FluentValidation.AspNetCore;
using InterviewApp.ApiClients.Clients.Implementation;
using InterviewApp.ApiClients.Clients.Interfaces;
using InterviewApp.ApiClients.Constants.Corezoid;
using InterviewApp.ApiClients.Constants.Imdb;
using InterviewApp.BackgroundTasks.Quartz.Constants;
using InterviewApp.BackgroundTasks.Quartz.Jobs;
using InterviewApp.BLL;
using InterviewApp.BLL.Services.Implementation;
using InterviewApp.BLL.Services.Interfaces;
using InterviewApp.Configuration;
using InterviewApp.Configuration.Constants;
using InterviewApp.DAL;
using InterviewApp.DAL.Repositories.Implementation;
using InterviewApp.DAL.Repositories.Interfaces;
using InterviewApp.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace InterviewApp.DependencyInjectionHelper
{
    public static class DependencyInjectionExtensions
    {
        public static void AddBusinessLogicDependencies(this IServiceCollection services)
        {
            services.AddScoped<IFilmService, FilmService>();
            services.AddScoped<IWatchlistService, WatchlistService>();
            services.AddScoped<IEmailService, EmailService>();
        }

        public static void AddApiClientsDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var handler = new HttpClientHandler();
            handler.MaxConnectionsPerServer = 2;

            var handler2 = new SocketsHttpHandler();
            handler2.PooledConnectionLifetime = TimeSpan.FromSeconds(60); //Аналог ConnectionLeaseTimeout
            handler2.PooledConnectionIdleTimeout = TimeSpan.FromSeconds(100); //Аналог MaxIdleTime

            var imdbClientName = configuration.GetValue<string>(AppSettingsConstants.ImdbClientName);
            services.AddHttpClient(imdbClientName, httpClient =>
            {
                httpClient.BaseAddress = new Uri(ImdbEndpointConstants.BaseUri);
            }).SetHandlerLifetime(TimeSpan.FromMinutes(1));
            services.AddScoped<IImdbClient, ImdbClient>();

            var corezoidClientName = configuration.GetValue<string>(AppSettingsConstants.CorezoidClientName);
            services.AddHttpClient(corezoidClientName, httpClient =>
            {
                httpClient.BaseAddress = new Uri(CorezoidEndpointConstants.BaseUri);
            }).SetHandlerLifetime(TimeSpan.FromMinutes(1));
            services.AddScoped<ICorezoidClient, CorezoidClient>();
        }

        public static void AddBackgroundTaskDependencies(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                q.AddJob<WatchlistEmailNotificationJob>(j =>
                {
                    j.WithIdentity(QuartzWatchlistConstants.JobName)
                        .Build();
                });

                q.AddTrigger(t =>
                {
                    t.WithIdentity(QuartzWatchlistConstants.SchedulerName)
                        .WithCronSchedule(QuartzWatchlistConstants.CronTriggerExpression)
                        .ForJob(QuartzWatchlistConstants.JobName);
                });
            });
            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        }

        public static void AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(AppSettingsConstants.InterviewAppDefaultConnection);
            services.AddDbContext<InterviewAppDbContext>(config =>
            {
                config.UseSqlServer(connectionString);
            });

            services.AddScoped<IWatchlistRepository, WatchlistRepository>();
            services.AddScoped<IInterviewAppUnitOfWork, InterviewAppUnitOfWork>();
        }

        public static void AddPresentationLayerDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IInterviewAppConfiguration, InterviewAppConfiguration>();

            services.AddAutoMapper(typeof(Startup), typeof(InterviewAppBllReference));
            
            services.AddControllers()
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<Startup>();
                });
        }
    }
}
