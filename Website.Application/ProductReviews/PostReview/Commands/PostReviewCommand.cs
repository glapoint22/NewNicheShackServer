using MediatR;
using Shared.Common.Classes;

namespace Website.Application.ProductReviews.PostReview.Commands
{
    public sealed record PostReviewCommand(string ProductId, int Rating, string Title, string Text) : IRequest<Result>;
}