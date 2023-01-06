using Manager.Application.Common.Interfaces;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;

namespace Manager.Application.Notifications.ReformList.EventHandlers
{
    public sealed class UserReceivedNoncompliantStrikeListEventHandler : INotificationHandler<UserReceivedNoncompliantStrikeListEvent>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IEmailService _emailService;

        public UserReceivedNoncompliantStrikeListEventHandler(IEmailService emailService, IManagerDbContext managerDbContext)
        {
            _emailService = emailService;
            _dbContext = managerDbContext;
        }

        public async Task Handle(UserReceivedNoncompliantStrikeListEvent notification, CancellationToken cancellationToken)
        {
            // Get the email from the database
            var email = await _dbContext.Emails
                .Where(x => x.Type == EmailType.NoncompliantStrikeList)
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
                },

                // List name
                Var1 = notification.ListName,

                // List description
                Var2 = notification.ListDescription!
            });

            // Send the email
            await _emailService.SendEmail(emailMessage);
        }
    }
}