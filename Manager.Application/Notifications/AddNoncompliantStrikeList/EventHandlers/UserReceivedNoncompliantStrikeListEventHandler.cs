using Manager.Application.Common.Interfaces;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using IEmailService = Manager.Application.Common.Interfaces.IEmailService;

namespace Manager.Application.Notifications.AddNoncompliantStrikeList.EventHandlers
{
    public sealed class UserReceivedNoncompliantStrikeListEventHandler : INotificationHandler<UserReceivedNoncompliantStrikeListEvent>
    {
        private readonly IWebsiteDbContext _websiteDbContext;
        private readonly IManagerDbContext _managerDbContext;
        private readonly IEmailService _emailService;

        public UserReceivedNoncompliantStrikeListEventHandler(IWebsiteDbContext dbContext, IEmailService emailService, IManagerDbContext managerDbContext)
        {
            _websiteDbContext = dbContext;
            _emailService = emailService;
            _managerDbContext = managerDbContext;
        }

        public async Task Handle(UserReceivedNoncompliantStrikeListEvent notification, CancellationToken cancellationToken)
        {
            var user = await _websiteDbContext.Users
                .Where(x => x.Id == notification.UserId)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Email
                }).SingleOrDefaultAsync();



            if (user != null)
            {
                // Get the email from the database
                var email = await _managerDbContext.Emails
                    .Where(x => x.Type == EmailType.NoncompliantStrikeList)
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
                    // Recipient
                    Recipient = new()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    },

                    // List name
                    Var1 = notification.ListName,

                    // List description
                    Var2 = notification.ListDescription!
                });

                // Send the email
                await _emailService.SendEmail(emailMessage);
            }
        }
    }
}