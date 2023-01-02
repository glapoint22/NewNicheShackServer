using Shared.EmailBuilder.Classes;

namespace Manager.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task<string> GetEmailBody(string emailContent);
        Task SendEmail(EmailMessage emailMessage);
    }
}