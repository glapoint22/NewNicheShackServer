using MediatR;
using Shared.Common.Entities;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.ProductReviews.PostReview.Commands
{
    public sealed class PostReviewCommandHandler : IRequestHandler<PostReviewCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public PostReviewCommandHandler(IUserService userService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(PostReviewCommand request, CancellationToken cancellationToken)
        {
            string userId = _userService.GetUserIdFromClaims();

            _dbContext.ProductReviews
                .Add(new ProductReview
                {
                    UserId = userId,
                    ProductId = request.ProductId,
                    Title = request.Title,
                    Rating = request.Rating,
                    Date = DateTime.UtcNow,
                    Text = request.Text
                });

            Product product = (await _dbContext.Products.FindAsync(request.ProductId))!;
            product.SetRating(request.Rating);


            // Update the product and save the changes to the database
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();

            product.AddDomainEvent(new PostedReviewEvent(userId, request.ProductId, request.Rating, request.Title, request.Text));

            return Result.Succeeded();
        }
    }
}