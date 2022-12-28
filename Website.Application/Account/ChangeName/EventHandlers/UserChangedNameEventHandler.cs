using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeName.EventHandlers
{
    public sealed class UserChangedNameEventHandler : INotificationHandler<UserChangedNameEvent>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;
        private readonly IEmailService _emailService;

        public UserChangedNameEventHandler(IUserService userService, IWebsiteDbContext dbContext, IEmailService emailService)
        {
            _userService = userService;
            _dbContext = dbContext;
            _emailService = emailService;
        }

        public async Task Handle(UserChangedNameEvent notification, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserByIdAsync(notification.UserId);

            // Get the email from the database
            string emailContent = await _dbContext.Emails
                .Where(x => x.Name == "Name Change")
                .Select(x => x.Content)
                .SingleAsync();

            // Create the email message
            EmailMessage emailMessage = new()
            {
                EmailBody = emailContent,
                EmailAddress = user.Email,
                Subject = "Name change confirmation",
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