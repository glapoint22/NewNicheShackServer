using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Application.ProductReviews.Classes;

namespace Website.Application.ProductReviews.GetPositiveNegativeReviews.Queries
{
    public sealed class GetPositiveNegativeReviewsQueryHandler : IRequestHandler<GetPositiveNegativeReviewsQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public GetPositiveNegativeReviewsQueryHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // ------------------------------------------------------------------------ Handle -----------------------------------------------------------------------------
        public async Task<Result> Handle(GetPositiveNegativeReviewsQuery request, CancellationToken cancellationToken)
        {
            ProductReviewDto? positiveReview = await GetPositiveReview(request.ProductId);
            ProductReviewDto? negativeReview = await GetNegativeReview(request.ProductId);

            return Result.Succeeded(new
            {
                positiveReview,
                negativeReview
            });
        }


        // ------------------------------------------------------------------ Get Positive Review ----------------------------------------------------------------------
        private async Task<ProductReviewDto?> GetPositiveReview(string productId)
        {
            return await _dbContext.ProductReviews
                .OrderByDescending(x => x.Rating)
                .ThenByDescending(x => x.Likes)
                .ThenByDescending(x => x.Date)
                .Where(x => x.Product.Id == productId && x.Likes > 0 && x.Rating > 3 && !x.Deleted)
                .Select()
                .FirstOrDefaultAsync();
        }



        // ------------------------------------------------------------------ Get Negative Review ----------------------------------------------------------------------
        private async Task<ProductReviewDto?> GetNegativeReview(string productId)
        {
            return await _dbContext.ProductReviews
                .OrderBy(x => x.Rating)
                .ThenByDescending(x => x.Likes)
                .ThenByDescending(x => x.Date)
                .Where(x => x.Product.Id == productId && x.Likes > 0 && x.Rating <= 3 && !x.Deleted)
                .Select()
                .FirstOrDefaultAsync();
        }
    }
}