using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.Lists.MoveProduct.EventHandlers
{
    public sealed class ProductMovedToListEventHandler : INotificationHandler<ProductMovedToListEvent>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IEmailService _emailService;

        public ProductMovedToListEventHandler(IWebsiteDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }


        public async Task Handle(ProductMovedToListEvent notification, CancellationToken cancellationToken)
        {
            List<Recipient> recipients;
            List<string> userIds;
            string emailBody;
            EmailMessage emailMessage;

            // Get the user that moved the item
            var user = await _dbContext.Users
                .Where(x => x.Id == notification.UserId)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Email,
                    x.EmailOnUserMovedListItem
                }).SingleAsync();


            // Get the source list name
            var sourceListName = await _dbContext.Lists
                .Where(x => x.Id == notification.SourceListId)
                .Select(x => x.Name)
                .SingleAsync();


            // Get the destination list name
            var destinationListName = await _dbContext.Lists
                .Where(x => x.Id == notification.DestinationListId)
                .Select(x => x.Name)
                .SingleAsync();



            // Get the product
            var product = await _dbContext.Products
                .Where(x => x.Id == notification.ProductId)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.UrlName,
                    Image = x.Media.ImageSm
                }).SingleAsync();


            // Get collaborators that are collaborating on both lists
            var collaborators = await _dbContext.Collaborators
                .Where(x => (x.ListId == notification.SourceListId ||
                    x.ListId == notification.DestinationListId) &&
                    x.UserId != notification.UserId)
                .GroupBy(x => x.UserId, (key, z) => new
                {
                    UserId = z.Select(x => x.UserId).First(),
                    Count = z.Count()
                }).ToListAsync();

            // If we have collaborators collaborating on both lists
            if (collaborators.Any(x => x.Count == 2))
            {
                userIds = collaborators
                    .Where(z => z.Count == 2)
                    .Select(z => z.UserId)
                    .ToList();

                recipients = await _dbContext.Collaborators
                    .Where(x => userIds
                        .Contains(x.UserId) &&
                        x.User.EmailOnCollaboratorMovedListItem == true)
                    .Select(x => new Recipient
                    {
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        Email = x.User.Email
                    })
                    .Distinct()
                    .ToListAsync();


                // If we have recipients
                if (recipients.Count > 0)
                {
                    // Get the email from the database
                    var email = await _dbContext.Emails
                        .Where(x => x.Type == EmailType.CollaboratorMovedListItem)
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

                            // User that removed the item
                            Person = new()
                            {
                                FirstName = user.FirstName,
                                LastName = user.LastName
                            },

                            // Product name
                            Var1 = product.Name,

                            // source List name
                            Var2 = sourceListName,

                            // destination List name
                            Var3 = destinationListName,

                            // Product link
                            Link = product.UrlName + "/" + product.Id,

                            // Product image
                            ImageName = product.Name,
                            ImageSrc = product.Image!
                        });

                        // Send the email
                        await _emailService.SendEmail(emailMessage);
                    }
                }
            }

            // Get user ids from collaborators that are collaborating on either the source list or the destination list
            userIds = collaborators
                .Where(z => z.Count == 1)
                .Select(z => z.UserId)
                .ToList();

            // Return if we have no collaborators
            if (userIds.Count == 0) return;


            // Get the recipients that are collaborating on the source list
            recipients = await _dbContext.Collaborators
                .Where(x => userIds
                    .Contains(x.UserId) &&
                    x.ListId == notification.SourceListId &&
                    x.User.EmailOnCollaboratorRemovedListItem == true)
                .Select(x => new Recipient
                {
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Email = x.User.Email
                }).ToListAsync();


            // If we have recipients
            if (recipients.Count > 0)
            {
                // Get the email content from the database
                var email = await _dbContext.Emails
                    .Where(x => x.Type == EmailType.CollaboratorRemovedListItem)
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

                        // User that removed the item
                        Person = new()
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName
                        },

                        // Product name
                        Var1 = product.Name,

                        // List name
                        Var2 = sourceListName,

                        // Product link
                        Link = product.UrlName + "/" + product.Id,

                        // Product image
                        ImageName = product.Name,
                        ImageSrc = product.Image!
                    });

                    // Send the email
                    await _emailService.SendEmail(emailMessage);
                }
            }



            // Get the recipients that are collaborating on the destination list
            recipients = await _dbContext.Collaborators
                .Where(x => userIds
                    .Contains(x.UserId) &&
                    x.ListId == notification.DestinationListId &&
                    x.User.EmailOnCollaboratorAddedListItem == true)
                .Select(x => new Recipient
                {
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Email = x.User.Email
                }).ToListAsync();



            // If we have recipients
            if (recipients.Count > 0)
            {
                // Get the email content from the database
                var email = await _dbContext.Emails
                    .Where(x => x.Type == EmailType.CollaboratorAddedListItem)
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

                        // User that added the item
                        Person = new()
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName
                        },

                        // Product name
                        Var1 = product.Name,

                        // List name
                        Var2 = destinationListName,

                        // Product link
                        Link = product.UrlName + "/" + product.Id,

                        // Product image
                        ImageName = product.Name,
                        ImageSrc = product.Image!
                    });

                    // Send the email
                    await _emailService.SendEmail(emailMessage);
                }
            }




            // Set up the email for the user that moved the list item
            if (user.EmailOnUserMovedListItem == true)
            {
                // Get the email content from the database
                var email = await _dbContext.Emails
                    .Where(x => x.Type == EmailType.UserMovedListItem)
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

                    // Product name
                    Var1 = product.Name,

                    // source List name
                    Var2 = sourceListName,

                    // destination List name
                    Var3 = destinationListName,

                    // Product link
                    Link = product.UrlName + "/" + product.Id,

                    // Product image
                    ImageName = product.Name,
                    ImageSrc = product.Image!
                });

                // Send the email
                await _emailService.SendEmail(emailMessage);
            }
        }
    }
}