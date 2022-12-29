using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.Account.DeleteAccount.EventHandlers
{
    public sealed class UserDeletedEventHandler : INotificationHandler<UserDeletedEvent>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IEmailService _emailService;

        public UserDeletedEventHandler(IWebsiteDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }


        public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
        {
            // Get the email from the database
            string emailContent = await _dbContext.Emails
                .Where(x => x.Name == "Delete Account")
                .Select(x => x.Content)
                .SingleAsync();


            // Get the email body
            string emailBody = await _emailService.GetEmailBody(emailContent);


            // Create the email message
            EmailMessage emailMessage = new(emailBody, notification.Email, "Account Deleted", new()
            {
                Recipient = new()
                {
                    FirstName = notification.FirstName,
                    LastName = notification.LastName
                }
            });

            // Send the email
            await _emailService.SendEmail(emailMessage);
        }
    }
}