using MediatR;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.RemoveReview.Commands
{
    public sealed class RemoveReviewCommandHandler : IRequestHandler<RemoveReviewCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public RemoveReviewCommandHandler(IWebsiteDbContext websiteDbContext)
        {
            _dbContext = websiteDbContext;
        }

        public async Task<Result> Handle(RemoveReviewCommand request, CancellationToken cancellationToken)
        {
            ProductReview? review = await _dbContext.ProductReviews.FindAsync(request.ReviewId);

            if (review != null)
            {
                review.Deleted = true;
            }

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}