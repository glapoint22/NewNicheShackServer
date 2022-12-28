using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.SignUp.EventHandlers
{
    public sealed class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IWebsiteDbContext _dbContext;

        public UserCreatedEventHandler(IUserService userService, IEmailService emailService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _emailService = emailService;
            _dbContext = dbContext;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserByIdAsync(notification.UserId);
            string otp = await _userService.GenerateEmailConfirmationTokenAsync(user);

            // Get the email from the database
            string emailContent = await _dbContext.Emails
                .Where(x => x.Name == "Account Activation")
                .Select(x => x.Content)
                .SingleAsync();


            // Create the email message
            EmailMessage emailMessage = new()
            {
                EmailBody = emailContent,
                EmailAddress = user.Email,
                Subject = "Activate Account",
                EmailProperties = new()
                {
                    Recipient = new()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    },
                    Var1 = otp
                }
            };

            // Send the email
            await _emailService.SendEmail(emailMessage);
        }
    }
}