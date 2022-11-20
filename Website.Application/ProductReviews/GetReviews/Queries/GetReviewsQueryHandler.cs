using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Application.ProductReviews.Classes;

namespace Website.Application.ProductReviews.GetReviews.Queries
{
    public sealed class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public GetReviewsQueryHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _dbContext.ProductReviews
                .SortBy(request.SortBy)
                .Where(x => x.ProductId == request.ProductId)
                .Where(request.FilterBy)
                .Select()
                .Skip((request.Page - 1) * 10)
                .Take(10)
                .ToListAsync();

            int totalReviews = await _dbContext.ProductReviews
                .Where(x => x.ProductId == request.ProductId)
                .Where(request.FilterBy)
                .CountAsync(cancellationToken);


            return Result.Succeeded(new
            {
                Reviews = reviews,
                TotalReviews = totalReviews,
                PageCount = Math.Max(1, Math.Ceiling((double)(totalReviews / 10f)))
            });
        }
    }
}