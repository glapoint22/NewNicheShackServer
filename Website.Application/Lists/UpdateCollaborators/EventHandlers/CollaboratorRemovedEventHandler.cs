using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.Lists.UpdateCollaborators.EventHandlers
{
    public sealed class CollaboratorRemovedEventHandler : INotificationHandler<CollaboratorRemovedEvent>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IEmailService _emailService;

        public CollaboratorRemovedEventHandler(IWebsiteDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }

        public async Task Handle(CollaboratorRemovedEvent notification, CancellationToken cancellationToken)
        {
            // Get the user that removed the collaborator
            var user = await _dbContext.Users
                .Where(x => x.Id == notification.UserId)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName
                }).SingleAsync();

            // Get the list name
            var listName = await _dbContext.Lists
                .Where(x => x.Id == notification.ListId)
                .Select(x => x.Name)
                .SingleAsync();

            // Get the collaborator that was removed
            var removedCollaborator = await _dbContext.Users
                .Where(x => x.Id == notification.CollaboratorUserId)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Email
                }).SingleAsync();


            // Get the email from the database
            string emailContent = await _dbContext.Emails
                .Where(x => x.Name == "Removed Collaborator")
                .Select(x => x.Content)
                .SingleAsync();


            // Create the email message
            EmailMessage emailMessage = new()
            {
                EmailBody = emailContent,
                EmailAddress = removedCollaborator.Email,
                Subject = "Removed from list",
                EmailProperties = new()
                {
                    Recipient = new()
                    {
                        FirstName = removedCollaborator.FirstName,
                        LastName = removedCollaborator.LastName
                    },

                    // Person who removed the collaborator
                    Person = new()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    },

                    // List name
                    Var1 = listName
                }
            };


            // Send the email
            await _emailService.SendEmail(emailMessage);


            // Get all other collaborators that are collaborating on this list
            var collaborators = await _dbContext.Collaborators
                .Where(x => x.ListId == notification.ListId && 
                    x.UserId != notification.UserId &&
                    x.UserId != notification.CollaboratorUserId &&
                    x.User.EmailOnRemovedCollaborator == true)
                .Select(x => new
                {
                    x.User.FirstName,
                    x.User.LastName,
                    x.User.Email
                }).ToListAsync();


            // Get the email from the database
            emailContent = await _dbContext.Emails
                .Where(x => x.Name == "Collaborator Removed")
                .Select(x => x.Content)
                .SingleAsync();


            // Send an email to the other collaborators
            foreach (var collaborator in collaborators)
            {
                // Create the email message
                emailMessage = new()
                {
                    EmailBody = emailContent,
                    EmailAddress = collaborator.Email,
                    Subject = "Collaborator has been removed from your list",
                    EmailProperties = new()
                    {
                        Recipient = new()
                        {
                            FirstName = collaborator.FirstName,
                            LastName = collaborator.LastName
                        },

                        // Collaborator that has been removed
                        Person = new()
                        {
                            FirstName = removedCollaborator.FirstName,
                            LastName = removedCollaborator.LastName
                        },

                        // List name
                        Var1 = listName
                    }
                };

                // Send the email
                await _emailService.SendEmail(emailMessage);
            }
        }
    }
}