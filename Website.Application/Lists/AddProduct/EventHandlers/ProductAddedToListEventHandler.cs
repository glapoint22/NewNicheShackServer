using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.Lists.AddProduct.EventHandlers
{
    public sealed class ProductAddedToListEventHandler : INotificationHandler<ProductAddedToListEvent>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IEmailService _emailService;

        public ProductAddedToListEventHandler(IWebsiteDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }

        public async Task Handle(ProductAddedToListEvent notification, CancellationToken cancellationToken)
        {
            EmailMessage emailMessage;

            // Get the user that added the item
            var user = await _dbContext.Users
                .Where(x => x.Id == notification.UserId)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Email
                }).SingleAsync();

            // Get all recipients from this list except the user that added the item
            List<Recipient> recipients = await _dbContext.Collaborators
                .Where(x => x.ListId == notification.ListId &&
                    x.UserId != notification.UserId &&
                    x.User.EmailOnAddedListItem == true)
                .Select(x => new Recipient
                {
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Email = x.User.Email
                }).ToListAsync();



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


            // Get the email content from the database
            string emailContent = await _dbContext.Emails
                .Where(x => x.Name == "Collaborator Added List Item")
                .Select(x => x.Content)
                .SingleAsync();


            // Get the email body
            string emailBody = await _emailService.GetEmailBody(emailContent);


            // Send the emails
            foreach (Recipient recipient in recipients)
            {
                // Create the email message
                emailMessage = new(emailBody, recipient.Email, "Item added to list", new()
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



            // Set up the email for the user that added the list item

            // Get the email content from the database
            emailContent = await _dbContext.Emails
                .Where(x => x.Name == "User Added List Item")
                .Select(x => x.Content)
                .SingleAsync();


            // Get the email body
            emailBody = await _emailService.GetEmailBody(emailContent);




            // Create the email message
            emailMessage = new(emailBody, user.Email, "Item added to list", new()
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