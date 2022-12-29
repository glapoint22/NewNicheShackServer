using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeEmail.EventHandlers
{
    public sealed class UserChangedEmailEventHandler : INotificationHandler<UserChangedEmailEvent>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IEmailService _emailService;

        public UserChangedEmailEventHandler(IWebsiteDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }


        public async Task Handle(UserChangedEmailEvent notification, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Where(x => x.Id == notification.UserId)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Email,
                    x.EmailOnEmailChange
                }).SingleAsync();


            if (user.EmailOnEmailChange == false) return;


            // Get the email from the database
            string emailContent = await _dbContext.Emails
                .Where(x => x.Name == "Email Change")
                .Select(x => x.Content)
                .SingleAsync();


            // Get the email body
            string emailBody = await _emailService.GetEmailBody(emailContent);


            // Create the email message
            EmailMessage emailMessage = new(emailBody, user.Email, "Email change confirmation", new()
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