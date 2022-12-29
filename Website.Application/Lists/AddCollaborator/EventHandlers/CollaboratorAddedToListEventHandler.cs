using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Lists.AddCollaborator.EventHandlers
{
    public sealed class CollaboratorAddedToListEventHandler : INotificationHandler<CollaboratorAddedToListEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IWebsiteDbContext _dbContext;

        public CollaboratorAddedToListEventHandler(IEmailService emailService, IWebsiteDbContext dbContext)
        {
            _emailService = emailService;
            _dbContext = dbContext;
        }


        public async Task Handle(CollaboratorAddedToListEvent notification, CancellationToken cancellationToken)
        {
            // Get the user that joined the list
            var user = await _dbContext.Users
                .Where(x => x.Id == notification.UserId)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Email,
                    x.EmailOnNewCollaborator
                }).SingleAsync();



            // Get the list name
            var listName = await _dbContext.Lists
                .Where(x => x.Id == notification.ListId)
                .Select(x => x.Name)
                .SingleAsync();




            // Get all other collaborators that are collaborating on this list
            var collaborators = await _dbContext.Collaborators
                .Where(x => x.ListId == notification.ListId &&
                    x.UserId != notification.UserId &&
                    x.User.EmailOnNewCollaborator == true)
                .Select(x => new
                {
                    x.User.FirstName,
                    x.User.LastName,
                    x.User.Email
                }).ToListAsync();


            // If we have collaborators
            if (collaborators.Count > 0)
            {
                // Get the email from the database
                string emailContent = await _dbContext.Emails
                    .Where(x => x.Name == "Collaborator Added To List")
                    .Select(x => x.Content)
                    .SingleAsync();




                // Get the email body
                string emailBody = await _emailService.GetEmailBody(emailContent);


                // Send an email to the other collaborators
                foreach (var collaborator in collaborators)
                {
                    // Create the email message
                    EmailMessage emailMessage = new(emailBody, collaborator.Email, "Collaborator has joined your list", new()
                    {
                        Recipient = new()
                        {
                            FirstName = collaborator.FirstName,
                            LastName = collaborator.LastName
                        },

                        // New collaborator
                        Person = new()
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName
                        },

                        // List name
                        Var1 = listName
                    });


                    // Send the email
                    await _emailService.SendEmail(emailMessage);
                }
            }



            // Set up the email for the user that joined the list
            if (user.EmailOnNewCollaborator == true)
            {
                // Get the email from the database
                string emailContent = await _dbContext.Emails
                    .Where(x => x.Name == "User Added To List")
                    .Select(x => x.Content)
                    .SingleAsync();




                // Get the email body
                string emailBody = await _emailService.GetEmailBody(emailContent);


                // Create the email message
                EmailMessage emailMessage = new(emailBody, user.Email, "You are collaborating on a list", new()
                {
                    Recipient = new()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    },

                    // List name
                    Var1 = listName
                });


                // Send the email
                await _emailService.SendEmail(emailMessage);
            }


        }
    }
}