using System;
using System.Linq;
using System.Threading.Tasks;
using InterviewApp.BLL.Models.EmailMessages;
using InterviewApp.BLL.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace InterviewApp.BackgroundTasks.Quartz.Jobs
{
    [DisallowConcurrentExecution]
    public class WatchlistEmailNotificationJob : IJob
    {
        private readonly ILogger<WatchlistEmailNotificationJob> _logger;
        private readonly IServiceProvider _serviceProvider;

        public WatchlistEmailNotificationJob(ILogger<WatchlistEmailNotificationJob> logger, 
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogTrace($"{nameof(WatchlistEmailNotificationJob)} started execution");

            try
            {
                var watchlistService = (IWatchlistService)_serviceProvider.GetService(typeof(IWatchlistService));
                var watchlistFilmEmailModel = await watchlistService.GetMostRatedFilmBetweenUnwatchedAndSendEmailAsync();
                var emailMessageModels = watchlistFilmEmailModel
                    .Select(w => new EmailMessageModel
                    {
                        ImageName = "filmImage.png",
                        ImageLink = w.PosterLink,
                        SubjectTitle = w.Title,
                        RecipientEmailAddress = "test@mail.com",
                        HtmlText = @$"<img src=""filmImage.png"" width=""250px"" height=""auto"">
                                  <h3>IMDb Rating: {w.ImdbRating}</h3>
                                  <p>{w.ShortDescriptionFromWiki}</p>"
                    });

                var emailService = (IEmailService)_serviceProvider.GetService(typeof(IEmailService));
                var emailSender = emailMessageModels.Select(e => emailService.SendEmailAsync(e));
                await Task.WhenAll(emailSender);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"{nameof(WatchlistEmailNotificationJob)} Exception {e.Message}");
            }

            _logger.LogTrace($"{nameof(WatchlistEmailNotificationJob)} finished execution");
        }
    }
}
