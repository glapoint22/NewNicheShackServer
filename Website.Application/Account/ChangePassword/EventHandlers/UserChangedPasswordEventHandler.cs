using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.ChangePassword.EventHandlers
{
    public sealed class UserChangedPasswordEventHandler : INotificationHandler<UserChangedPasswordEvent>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;
        private readonly IEmailService _emailService;

        public UserChangedPasswordEventHandler(IUserService userService, IWebsiteDbContext dbContext, IEmailService emailService)
        {
            _userService = userService;
            _dbContext = dbContext;
            _emailService = emailService;
        }


        public async Task Handle(UserChangedPasswordEvent notification, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserByIdAsync(notification.UserId);

            // Get the email from the database
            string emailContent = await _dbContext.Emails
                .Where(x => x.Name == "Password Change")
                .Select(x => x.Content)
                .SingleAsync();

            // Create the email message
            EmailMessage emailMessage = new()
            {
                EmailBody = emailContent,
                EmailAddress = user.Email,
                Subject = "Password change confirmation",
                EmailProperties = new()
                {
                    Recipient = new()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    }
                }
            };

            // Send the email
            await _emailService.SendEmail(emailMessage);
        }
    }
}