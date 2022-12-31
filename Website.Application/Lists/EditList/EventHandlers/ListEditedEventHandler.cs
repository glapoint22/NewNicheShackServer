using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.Lists.EditList.EventHandlers
{
    public sealed class ListEditedEventHandler : INotificationHandler<ListEditedEvent>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IEmailService _emailService;

        public ListEditedEventHandler(IWebsiteDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }


        public async Task Handle(ListEditedEvent notification, CancellationToken cancellationToken)
        {
            EmailMessage emailMessage;
            string emailBody;


            // Get the user that updated the list
            var user = await _dbContext.Users
                .Where(x => x.Id == notification.UserId)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Email,
                    x.EmailOnUserUpdatedList
                }).SingleAsync();



            // Get all recipients from this list except the user that updated the list
            List<Recipient> recipients = await _dbContext.Collaborators
                .Where(x => x.ListId == notification.ListId &&
                    x.UserId != notification.UserId &&
                    x.User.EmailOnCollaboratorUpdatedList == true)
                .Select(x => new Recipient
                {
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Email = x.User.Email
                }).ToListAsync();


            if (recipients.Count > 0)
            {
                // Get the email from the database
                var email = await _dbContext.Emails
                    .Where(x => x.Type == EmailType.CollaboratorUpdatedList)
                    .Select(x => new
                    {
                        x.Name,
                        x.Content
                    }).SingleAsync();


                // Get the email body
                emailBody = await _emailService.GetEmailBody(email.Content);



                // Send the emails
                foreach (Recipient recipient in recipients)
                {
                    // Create the email message
                    emailMessage = new(emailBody, recipient.Email, email.Name, new()
                    {
                        // Recipient
                        Recipient = new()
                        {
                            FirstName = recipient.FirstName,
                            LastName = recipient.LastName
                        },

                        // User that updated the list
                        Person = new()
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName
                        },

                        // previous name
                        Var1 = notification.PreviousName,

                        // Previous description
                        Var2 = notification.PreviousDescription ?? "",

                        // New name
                        Var3 = notification.NewName,

                        // New description
                        Var4 = notification.NewDescription ?? ""
                    });

                    // Send the email
                    await _emailService.SendEmail(emailMessage);
                }
            }







            // Set up the email for the user that updated the list
            if (user.EmailOnUserUpdatedList == true)
            {
                // Get the email content from the database
                var email = await _dbContext.Emails
                    .Where(x => x.Type == EmailType.UserUpdatedList)
                    .Select(x => new
                    {
                        x.Name,
                        x.Content
                    }).SingleAsync();


                // Get the email body
                emailBody = await _emailService.GetEmailBody(email.Content);


                // Create the email message
                emailMessage = new(emailBody, user.Email, email.Name, new()
                {
                    // Recipient
                    Recipient = new()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    },

                    // previous name
                    Var1 = notification.PreviousName,

                    // Previous description
                    Var2 = notification.PreviousDescription ?? "",

                    // New name
                    Var3 = notification.NewName,

                    // New description
                    Var4 = notification.NewDescription ?? ""
                });

                // Send the email
                await _emailService.SendEmail(emailMessage);
            }
        }
    }
}