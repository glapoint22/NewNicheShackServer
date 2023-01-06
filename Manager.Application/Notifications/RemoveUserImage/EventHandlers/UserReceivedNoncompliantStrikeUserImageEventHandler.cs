using Manager.Application.Common.Interfaces;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;

namespace Manager.Application.Notifications.RemoveUserImage.EventHandlers
{
    public sealed class UserReceivedNoncompliantStrikeUserImageEventHandler : INotificationHandler<UserReceivedNoncompliantStrikeUserImageEvent>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IEmailService _emailService;


        public UserReceivedNoncompliantStrikeUserImageEventHandler(IManagerDbContext managerDbContext, IEmailService emailService)
        {
            _dbContext = managerDbContext;
            _emailService = emailService;
        }


        public async Task Handle(UserReceivedNoncompliantStrikeUserImageEvent notification, CancellationToken cancellationToken)
        {
            // Get the email from the database
            var email = await _dbContext.Emails
                .Where(x => x.Type == EmailType.NoncompliantStrikeUserImage)
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

                // User image
                ImageName = notification.FirstName + " " + notification.LastName,
                ImageSrc = notification.UserImage
            });

            // Send the email
            await _emailService.SendEmail(emailMessage);
        }
    }
}