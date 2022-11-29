using Manager.Application.Common.Classes;
using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.SearchProducts.Queries
{
    public sealed class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SearchProductsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _dbContext.Products
                .Where(request.SearchTerm)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    Link = x.UrlName + "/" + x.Id
                }).ToListAsync();

            return Result.Succeeded(products);
        }
    }
}