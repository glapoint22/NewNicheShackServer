using Manager.Application.Common.Interfaces;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.EmailBuilder.Classes;

namespace Manager.Application.Notifications.SendEmail.EventHandlers
{
    public sealed class UserSentEmailEventHandler : INotificationHandler<UserSentEmailEvent>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IEmailService _emailService;

        public UserSentEmailEventHandler(IManagerDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }


        public async Task Handle(UserSentEmailEvent notification, CancellationToken cancellationToken)
        {
            // Get the email from the database
            var email = await _dbContext.Emails
                .Where(x => x.Type == EmailType.MessageReply)
                .Select(x => new
                {
                    x.Name,
                    x.Content
                }).SingleAsync();


            // Get the email body
            string emailBody = await _emailService.GetEmailBody(email.Content);

            // Create the email message
            EmailMessage emailMessage = new(emailBody, notification.RecipientEmailAddress, "[Niche Shack] Re: Conversation with " + notification.RecipientName, new()
            {
                // Recipient
                Recipient = new()
                {
                    FirstName = notification.RecipientName,
                },

                Var1 = Utility.TextToHTML(notification.RecipientEmailMessage),

                Var2 = Utility.TextToHTML(notification.EmployeeEmailMessage),
            }, notification.EmployeeEmailAddress);

            // Send the email
            await _emailService.SendEmail(emailMessage);
        }
    }
}