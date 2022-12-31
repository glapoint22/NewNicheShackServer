using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.Account.ChangePassword.EventHandlers
{
    public sealed class UserChangedPasswordEventHandler : INotificationHandler<UserChangedPasswordEvent>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IEmailService _emailService;

        public UserChangedPasswordEventHandler(IWebsiteDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }


        public async Task Handle(UserChangedPasswordEvent notification, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Where(x => x.Id == notification.UserId)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Email,
                    x.EmailOnPasswordUpdated
                }).SingleAsync();

            if (user.EmailOnPasswordUpdated == false) return;

            // Get the email from the database
            var email = await _dbContext.Emails
                .Where(x => x.Type == EmailType.PasswordUpdated)
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
                }
            });


            // Send the email
            await _emailService.SendEmail(emailMessage);
        }
    }
}