using Shared.EmailBuilder.Classes;

namespace Website.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(EmailMessage emailMessage);
    }
}