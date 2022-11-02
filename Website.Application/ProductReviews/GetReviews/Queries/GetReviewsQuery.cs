using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.ProductReviews.GetReviews.Queries
{
    public sealed record GetReviewsQuery(string ProductId, int Page, string SortBy, string FilterBy) : IRequest<Result>;
}