using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.Lists.DeleteList.EventHandlers
{
    public sealed class ListDeletedEventHandler : INotificationHandler<ListDeletedEvent>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IEmailService _emailService;

        public ListDeletedEventHandler(IWebsiteDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }


        public async Task Handle(ListDeletedEvent notification, CancellationToken cancellationToken)
        {
            EmailMessage emailMessage;
            string emailContent;
            string emailBody;

            // Get the user that deleted the list
            var user = await _dbContext.Users
                .Where(x => x.Id == notification.UserId)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Email,
                    x.EmailOnDeletedList
                }).SingleAsync();


            if (notification.Collaborators.Count > 0)
            {
                // Get all recipients from this list
                List<Recipient> recipients = await _dbContext.Users
                    .Where(x => notification.Collaborators.Contains(x.Id))
                    .Select(x => new Recipient
                    {
                        UserId = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email
                    }).ToListAsync();



                // Get the email content from the database
                emailContent = await _dbContext.Emails
                    .Where(x => x.Name == "Collaborator Deleted List")
                    .Select(x => x.Content)
                    .SingleAsync();


                // Get the email body
                emailBody = await _emailService.GetEmailBody(emailContent);


                // Send the emails
                foreach (Recipient recipient in recipients)
                {
                    // Create the email message
                    emailMessage = new(emailBody, recipient.Email, "List has been deleted", new()
                    {
                        // Recipient
                        Recipient = new()
                        {
                            FirstName = recipient.FirstName,
                            LastName = recipient.LastName
                        },

                        // User that deleted the list
                        Person = new()
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName
                        },

                        // List name
                        Var1 = notification.ListName
                    });

                    // Send the email
                    await _emailService.SendEmail(emailMessage);
                }
            }







            // Set up the email for the user that deleted the list
            if (user.EmailOnDeletedList == true)
            {
                // Get the email content from the database
                emailContent = await _dbContext.Emails
                    .Where(x => x.Name == "User Deleted List")
                    .Select(x => x.Content)
                    .SingleAsync();


                // Get the email body
                emailBody = await _emailService.GetEmailBody(emailContent);



                // Create the email message
                emailMessage = new(emailBody, user.Email, "List has been deleted", new()
                {
                    // Recipient
                    Recipient = new()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    },

                    // List name
                    Var1 = notification.ListName
                });

                // Send the email
                await _emailService.SendEmail(emailMessage);
            }
        }
    }
}