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
                    x.LastName
                }).SingleAsync();

            // Get all recipients from this list except the user that removed the item
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
                .Where(x => x.Name == "Removed List Item")
                .Select(x => x.Content)
                .SingleAsync();


            // Get the email body
            string emailBody = await _emailService.GetEmailBody(emailContent);


            // Send the emails
            foreach (Recipient recipient in recipients)
            {
                // Create the email message
                EmailMessage emailMessage = new(emailBody, recipient.Email, "Item removed from list", new()
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
                    Var2 = listName,

                    // Product link
                    Link = product.UrlName + "/" + product.Id,

                    // Product image
                    ImageName = product.Name,
                    ImageSrc = product.Image!,

                    // Host
                    Host = _emailService.GetHost()
                });

                // Send the email
                await _emailService.SendEmail(emailMessage);
            }
        }
    }
}