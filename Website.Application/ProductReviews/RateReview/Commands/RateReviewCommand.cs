using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.ProductReviews.RateReview.Commands
{
    public sealed record RateReviewCommand(int ReviewId, int Likes, int Dislikes) : IRequest<Result>;
}