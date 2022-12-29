using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.ProductReviews.PostReview.EventHandlers
{
    public sealed class PostedReviewEventHandler : INotificationHandler<PostedReviewEvent>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IEmailService _emailService;

        public PostedReviewEventHandler(IWebsiteDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }


        public async Task Handle(PostedReviewEvent notification, CancellationToken cancellationToken)
        {
            // Get the user that posted the review
            var user = await _dbContext.Users
                .Where(x => x.Id == notification.UserId)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Email
                }).SingleAsync();


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


            // Get the review
            var review = await _dbContext.ProductReviews
                .Where(x => x.Id == notification.ReviewId)
                .Select(x => new
                {
                    x.Title,
                    x.Text,
                    x.Rating
                }).SingleAsync();



            // Get the email content from the database
            string emailContent = await _dbContext.Emails
                .Where(x => x.Name == "Review")
                .Select(x => x.Content)
                .SingleAsync();


            // Get the email body
            string emailBody = await _emailService.GetEmailBody(emailContent);

            // Get the stars image
            string stars = await GetStarsImage(review.Rating);

            // Create the email message
            EmailMessage emailMessage = new(emailBody, user.Email, "Review", new()
            {
                // Recipient
                Recipient = new()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName
                },

                // Product name
                Var1 = product.Name,

                // Review title
                Var2 = review.Title,

                // Review text
                Var3 = review.Text,

                // Product link
                Link = product.UrlName + "/" + product.Id,

                // Product image
                ImageName = product.Name,
                ImageSrc = product.Image!,

                Stars = stars
            });

            // Send the email
            await _emailService.SendEmail(emailMessage);
        }






        private async Task<string> GetStarsImage(double rating)
        {
            string imageName = string.Empty;

            switch (rating)
            {
                case 1:
                    imageName = "One Star";
                    break;

                case 2:
                    imageName = "Two Stars";
                    break;

                case 3:
                    imageName = "Three Stars";
                    break;

                case 4:
                    imageName = "Four Stars";
                    break;

                case 5:
                    imageName = "Five Stars";
                    break;
            }


            return await _dbContext.Media
                .Where(x => x.Name == imageName)
                .Select(x => x.ImageAnySize!)
                .SingleAsync();
        }
    }
}