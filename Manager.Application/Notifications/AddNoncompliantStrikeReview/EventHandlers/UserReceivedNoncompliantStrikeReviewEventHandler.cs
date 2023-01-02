using Manager.Application.Common.Interfaces;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;

namespace Manager.Application.Notifications.AddNoncompliantStrikeReview.EventHandlers
{
    public sealed class UserReceivedNoncompliantStrikeReviewEventHandler : INotificationHandler<UserReceivedNoncompliantStrikeReviewEvent>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IEmailService _emailService;

        public UserReceivedNoncompliantStrikeReviewEventHandler(IManagerDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }

        public async Task Handle(UserReceivedNoncompliantStrikeReviewEvent notification, CancellationToken cancellationToken)
        {
            // Get the email from the database
            var email = await _dbContext.Emails
                .Where(x => x.Type == EmailType.NoncompliantStrikeReview)
                .Select(x => new
                {
                    x.Name,
                    x.Content
                }).SingleAsync();


            // Get the email body
            string emailBody = await _emailService.GetEmailBody(email.Content);


            // Create the email message
            EmailMessage emailMessage = new(emailBody, notification.Email, email.Name, new()
            {
                // Recipient
                Recipient = new()
                {
                    FirstName = notification.FirstName,
                    LastName = notification.LastName
                },

                // Review title
                Var1 = notification.Title,

                // Review text
                Var2 = notification.Text
            });

            // Send the email
            await _emailService.SendEmail(emailMessage);
        }
    }
}