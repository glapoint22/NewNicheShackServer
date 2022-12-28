using Shared.EmailBuilder.Classes;

namespace Website.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task<string> GetEmailBody(string emailContent);
        Task SendEmail(EmailMessage emailMessage);
        string GetHost();
    }
}