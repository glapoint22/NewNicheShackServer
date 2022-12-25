using MediatR;
using Shared.Common.Classes;

namespace Website.Application.ProductReviews.GetReviews.Queries
{
    public sealed record GetReviewsQuery(Guid ProductId, int Page, string? SortBy, string? FilterBy) : IRequest<Result>;
}