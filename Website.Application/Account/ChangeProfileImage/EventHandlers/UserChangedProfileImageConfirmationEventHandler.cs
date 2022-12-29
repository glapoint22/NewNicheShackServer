using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeProfileImage.EventHandlers
{
    public sealed class UserChangedProfileImageConfirmationEventHandler : INotificationHandler<UserChangedProfileImageEvent>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IEmailService _emailService;

        public UserChangedProfileImageConfirmationEventHandler(IWebsiteDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }

        public async Task Handle(UserChangedProfileImageEvent notification, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Where(x => x.Id == notification.UserId)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Email,
                    x.EmailOnProfileImageChange,
                    x.Image
                }).SingleAsync();


            if (user.EmailOnProfileImageChange == false) return;


            // Get the email from the database
            string emailContent = await _dbContext.Emails
                .Where(x => x.Name == "Profile Image Change")
                .Select(x => x.Content)
                .SingleAsync();


            // Get the email body
            string emailBody = await _emailService.GetEmailBody(emailContent);


            // Create the email message
            EmailMessage emailMessage = new(emailBody, user.Email, "Profile Image change confirmation", new()
            {
                Recipient = new()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName
                },
                ImageName = user.FirstName + " " + user.LastName,
                ImageSrc = user.Image!
            });


            // Send the email
            await _emailService.SendEmail(emailMessage);
        }
    }
}