using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.CreateDeleteAccountOTP.EventHandlers
{
    public sealed class UserDeletedAccountOtpEventHandler : INotificationHandler<UserDeletedAccountOtpEvent>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;
        private readonly IEmailService _emailService;

        public UserDeletedAccountOtpEventHandler(IUserService userService, IWebsiteDbContext dbContext, IEmailService emailService)
        {
            _userService = userService;
            _dbContext = dbContext;
            _emailService = emailService;
        }


        public async Task Handle(UserDeletedAccountOtpEvent notification, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserByIdAsync(notification.UserId);
            string otp = await _userService.GenerateDeleteAccountTokenAsync(user);


            // Get the email from the database
            var email = await _dbContext.Emails
                .Where(x => x.Type == EmailType.DeleteAccountOneTimePassword)
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
                Recipient = new()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName
                },
                Var1 = otp
            });


            // Send the email
            await _emailService.SendEmail(emailMessage);
        }
    }
}