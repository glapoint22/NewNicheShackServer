using Manager.Application.Common.Interfaces;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;

namespace Manager.Application.Notifications.ReplaceUserName.EventHandlers
{
    public sealed class UserReceivedNoncompliantStrikeUserNameEventHandler : INotificationHandler<UserReceivedNoncompliantStrikeUserNameEvent>
    {
        private readonly IManagerDbContext _managerDbContext;
        private readonly IEmailService _emailService;

        public UserReceivedNoncompliantStrikeUserNameEventHandler(IManagerDbContext managerDbContext, IEmailService emailService)
        {
            _managerDbContext = managerDbContext;
            _emailService = emailService;
        }


        public async Task Handle(UserReceivedNoncompliantStrikeUserNameEvent notification, CancellationToken cancellationToken)
        {
            // Get the email from the database
            var email = await _managerDbContext.Emails
                .Where(x => x.Type == EmailType.NoncompliantStrikeUserName)
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