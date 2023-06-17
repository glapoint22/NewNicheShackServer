using Manager.Application.Common.Interfaces;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;

namespace Manager.Application.Common.EventHandlers
{
    public sealed class UserAccountTerminatedEventHandler : INotificationHandler<UserAccountTerminatedEvent>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IEmailService _emailService;

        public UserAccountTerminatedEventHandler(IManagerDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }


        public async Task Handle(UserAccountTerminatedEvent notification, CancellationToken cancellationToken)
        {
            // Get the email from the database
            var email = await _dbContext.Emails
                .Where(x => x.Type == EmailType.UserAccountTerminated)
                .Select(x => new
                {
                    x.Name,
                    x.Content
                }).SingleAsync();

            // Get the email body
            string emailBody = await _emailService.GetEmailBody(email.Content);


            // Create the email message
            EmailMessage emailMessage = new(emailBody, notification.Email, email.Name, new()
            {
                // Recipient
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
