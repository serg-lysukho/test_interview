using System.Threading.Tasks;
using InterviewApp.BLL.Models.EmailMessages;

namespace InterviewApp.BLL.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessageModel model);
    }
}
