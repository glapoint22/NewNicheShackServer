using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.ProductReviews.RateReview.Commands
{
    public sealed record RateReviewCommand(Guid ReviewId, int Likes, int Dislikes) : IRequest<Result>;
}