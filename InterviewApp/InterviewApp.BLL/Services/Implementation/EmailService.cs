using System.Net;
using System.Threading.Tasks;
using InterviewApp.BLL.Constants.EmailMessage;
using InterviewApp.BLL.Models.EmailMessages;
using InterviewApp.BLL.Services.Interfaces;
using InterviewApp.Configuration;
using MailKit.Net.Smtp;
using MimeKit;

namespace InterviewApp.BLL.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly IInterviewAppConfiguration _configuration;

        public EmailService(IInterviewAppConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(EmailMessageModel model)
        {
            var emailMessage = new MimeMessage();

            var senderEmailAddress = new MailboxAddress
            (
                _configuration.SmtpSenderName,
                _configuration.SmtpSenderMail
            );
            emailMessage.From.Add(senderEmailAddress);
            emailMessage.To.Add(new MailboxAddress(string.Empty, model.RecipientEmailAddress));
            emailMessage.Subject = model.SubjectTitle;

            var builder = new BodyBuilder();

            if (!string.IsNullOrEmpty(model.ImageLink))
            {
                var imageName = string.IsNullOrEmpty(model.ImageName) ? 
                    EmailMessageConstants.DefaultFilmPosterImageName : 
                    model.ImageName;

                using var webClient = new WebClient();
                var byteArray = webClient.DownloadData(model.ImageLink);

                builder.LinkedResources.Add(imageName, byteArray);
            }

            builder.HtmlBody = model.HtmlText;
            emailMessage.Body = builder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_configuration.SmtpHost, _configuration.SmtpPort, true);
            await client.AuthenticateAsync(_configuration.SmtpSenderMail, _configuration.MailPassword);

            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
