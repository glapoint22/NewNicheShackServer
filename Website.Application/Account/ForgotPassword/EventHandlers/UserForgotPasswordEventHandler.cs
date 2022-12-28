using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.ForgotPassword.EventHandlers
{
    public sealed class UserForgotPasswordEventHandler : INotificationHandler<UserForgotPasswordEvent>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;
        private readonly IEmailService _emailService;

        public UserForgotPasswordEventHandler(IUserService userService, IWebsiteDbContext dbContext, IEmailService emailService)
        {
            _userService = userService;
            _dbContext = dbContext;
            _emailService = emailService;
        }

        public async Task Handle(UserForgotPasswordEvent notification, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserByIdAsync(notification.UserId);
            string otp = await _userService.GeneratePasswordResetTokenAsync(user);

            // Get the email from the database
            string emailContent = await _dbContext.Emails
                .Where(x => x.Name == "Forgot Password")
                .Select(x => x.Content)
                .SingleAsync();

            // Create the email message
            EmailMessage emailMessage = new()
            {
                EmailBody = emailContent,
                EmailAddress = user.Email,
                Subject = "Forgot Password",
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