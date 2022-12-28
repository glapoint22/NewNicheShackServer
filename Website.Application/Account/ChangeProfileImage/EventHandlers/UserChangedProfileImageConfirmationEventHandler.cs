using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeProfileImage.EventHandlers
{
    public sealed class UserChangedProfileImageConfirmationEventHandler : INotificationHandler<UserChangedProfileImageEvent>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;
        private readonly IEmailService _emailService;

        public UserChangedProfileImageConfirmationEventHandler(IUserService userService, IWebsiteDbContext dbContext, IEmailService emailService)
        {
            _userService = userService;
            _dbContext = dbContext;
            _emailService = emailService;
        }


        public async Task Handle(UserChangedProfileImageEvent notification, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserByIdAsync(notification.UserId);

            // Get the email from the database
            string emailContent = await _dbContext.Emails
                .Where(x => x.Name == "Profile Image Change")
                .Select(x => x.Content)
                .SingleAsync();

            // Create the email message
            EmailMessage emailMessage = new()
            {
                EmailBody = emailContent,
                EmailAddress = user.Email,
                Subject = "Profile Image change confirmation",
                EmailProperties = new()
                {
                    Recipient = new()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    },
                    ImageName = user.FirstName + " " + user.LastName,
                    ImageSrc = user.Image!
                }
            };

            // Send the email
            await _emailService.SendEmail(emailMessage);
        }
    }
}