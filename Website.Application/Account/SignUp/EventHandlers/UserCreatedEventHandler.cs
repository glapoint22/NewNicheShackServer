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
            string token = await _userService.GenerateEmailConfirmationTokenAsync(user);
            string emailContent = await _dbContext.Emails
                .Where(x => x.Name == "Account Activation")
                .Select(x => x.Content)
                .SingleAsync();

            EmailMessage emailMessage = new()
            {
                EmailContent = emailContent,
                EmailAddress = user.Email,
                Subject = "Activate Account",
                EmailProperties = new()
                {
                    Recipient = new()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    },
                    Var1 = token
                }
            };

            await _emailService.SendEmail(emailMessage);
        }
    }
}