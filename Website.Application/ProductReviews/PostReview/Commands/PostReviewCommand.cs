using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.ProductReviews.PostReview.Commands
{
    public sealed record PostReviewCommand(int ProductId, int Rating, string Title, string Text) : IRequest<Result>;
}