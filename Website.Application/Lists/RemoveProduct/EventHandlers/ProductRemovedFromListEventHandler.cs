using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.Lists.RemoveProduct.EventHandlers
{
    public sealed class ProductRemovedFromListEventHandler : INotificationHandler<ProductRemovedFromListEvent>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IEmailService _emailService;

        public ProductRemovedFromListEventHandler(IWebsiteDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }


        public async Task Handle(ProductRemovedFromListEvent notification, CancellationToken cancellationToken)
        {
            // Get the user that removed the item
            var user = await _dbContext.Users
                .Where(x => x.Id == notification.UserId)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Email,
                    x.EmailOnUserRemovedListItem
                }).SingleAsync();





            // Get the list name
            var listName = await _dbContext.Lists
                .Where(x => x.Id == notification.ListId)
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




            // Get all recipients from this list except the user that removed the item
            List<Recipient> recipients = await _dbContext.Collaborators
                .Where(x => x.ListId == notification.ListId &&
                    x.UserId != notification.UserId &&
                    x.User.EmailOnCollaboratorRemovedListItem == true)
                .Select(x => new Recipient
                {
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Email = x.User.Email
                }).ToListAsync();



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
                string emailBody = await _emailService.GetEmailBody(email.Content);


                // Send the emails
                foreach (Recipient recipient in recipients)
                {
                    // Create the email message
                    EmailMessage emailMessage = new(emailBody, recipient.Email, email.Name, new()
                    {
                        // Recipient
                        Recipient = new()
                        {
                            FirstName = recipient.FirstName,
                            LastName = recipient.LastName
                        },

                        // Collaborator that removed the item
                        Person = new()
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName
                        },

                        // Product name
                        Var1 = product.Name,

                        // List name
                        Var2 = listName,

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




            // Set up the email for the user that removed the list item
            if (user.EmailOnUserRemovedListItem == true)
            {
                // Get the email content from the database
                var email = await _dbContext.Emails
                    .Where(x => x.Type == EmailType.UserRemovedListItem)
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

                    // Product name
                    Var1 = product.Name,

                    // List name
                    Var2 = listName,

                    // Product link
                    Link = product.UrlName + "/" + product.Id,

                    // Product image
                    ImageName = product.Name,
                    ImageSrc = product.Image!,
                });

                // Send the email
                await _emailService.SendEmail(emailMessage);
            }
        }
    }
}