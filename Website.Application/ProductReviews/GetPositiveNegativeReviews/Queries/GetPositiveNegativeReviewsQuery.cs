using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.ProductReviews.GetPositiveNegativeReviews.Queries
{
    public sealed record GetPositiveNegativeReviewsQuery(int ProductId) : IRequest<Result>;
}