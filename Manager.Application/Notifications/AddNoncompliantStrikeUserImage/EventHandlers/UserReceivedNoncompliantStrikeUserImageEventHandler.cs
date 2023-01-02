using Manager.Application.Common.Interfaces;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using IEmailService = Manager.Application.Common.Interfaces.IEmailService;

namespace Manager.Application.Notifications.AddNoncompliantStrikeUserImage.EventHandlers
{
    public sealed class UserReceivedNoncompliantStrikeUserImageEventHandler : INotificationHandler<UserReceivedNoncompliantStrikeUserImageEvent>
    {
        private readonly IWebsiteDbContext _websiteDbContext;
        private readonly IManagerDbContext _managerDbContext;
        private readonly IEmailService _emailService;


        public UserReceivedNoncompliantStrikeUserImageEventHandler(IWebsiteDbContext websiteDbContext, IManagerDbContext managerDbContext, IEmailService emailService)
        {
            _websiteDbContext = websiteDbContext;
            _managerDbContext = managerDbContext;
            _emailService = emailService;
        }


        public async Task Handle(UserReceivedNoncompliantStrikeUserImageEvent notification, CancellationToken cancellationToken)
        {
            var user = await _websiteDbContext.Users
                .Where(x => x.Id == notification.UserId)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Email
                }).SingleOrDefaultAsync();

            if (user != null)
            {
                // Get the email from the database
                var email = await _managerDbContext.Emails
                    .Where(x => x.Type == EmailType.NoncompliantStrikeUserImage)
                    .Select(x => new
                    {
                        x.Name,
                        x.Content
                    }).SingleAsync();


                // Get the email body
                string emailBody = await _emailService.GetEmailBody(email.Content);


                // Create the email message
                EmailMessage emailMessage = new(emailBody, user.Email, email.Name, new()
                {
                    // Recipient
                    Recipient = new()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    },

                    // User image
                    ImageName = user.FirstName + " " + user.LastName,
                    ImageSrc = notification.UserImage
                });

                // Send the email
                await _emailService.SendEmail(emailMessage);
            }
        }
    }
}