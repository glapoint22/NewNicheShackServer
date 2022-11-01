using MediatR;
using Shared.Common.Entities;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.ProductReviews.RateReview.Commands
{
    public sealed class RateReviewCommandHandler : IRequestHandler<RateReviewCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public RateReviewCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(RateReviewCommand request, CancellationToken cancellationToken)
        {
            ProductReview? review = await _dbContext.ProductReviews.FindAsync(request.ReviewId);

            if (review == null) return Result.Failed();

            // Increment the likes or dislikes
            review.Likes += request.Likes;
            review.Dislikes += request.Dislikes;

            // Update and save
            _dbContext.ProductReviews.Update(review);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}