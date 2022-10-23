using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.ProductReviews.Queries
{
    public sealed class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, Result>
    {
        public Task<Result> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}