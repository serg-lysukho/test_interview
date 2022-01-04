using InterviewApp.Configuration.Constants;
using Microsoft.Extensions.Configuration;

namespace InterviewApp.Configuration
{
    public class InterviewAppConfiguration : IInterviewAppConfiguration
    {
        private readonly IConfiguration _configuration;

        public InterviewAppConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ImdbApiKey  => _configuration.GetValue<string>(AppSettingsConstants.ImdbApiKey);
        public string ImdbClientName => _configuration.GetValue<string>(AppSettingsConstants.ImdbClientName);

        public string CorezoidApiLogin => _configuration.GetValue<string>(AppSettingsConstants.CorezoidApiLogin);
        public string CorezoidApiKey => _configuration.GetValue<string>(AppSettingsConstants.CorezoidApiKey);
        public string CorezoidClientName => _configuration.GetValue<string>(AppSettingsConstants.CorezoidClientName);
        
        public string SmtpSenderName => _configuration.GetValue<string>(AppSettingsConstants.SmtpSenderName);
        public string SmtpSenderMail => _configuration.GetValue<string>(AppSettingsConstants.SmtpSenderMail);
        public string SmtpHost => _configuration.GetValue<string>(AppSettingsConstants.SmtpHost);
        public int SmtpPort => _configuration.GetValue<int>(AppSettingsConstants.SmtpPort);
        public string MailPassword => _configuration.GetValue<string>(AppSettingsConstants.MailPassword);
    }
}
