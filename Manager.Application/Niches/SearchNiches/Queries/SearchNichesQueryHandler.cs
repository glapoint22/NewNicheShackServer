using Manager.Application.Common.Classes;
using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.QueryBuilder;

namespace Manager.Application.Niches.SearchNiches.Queries
{
    public sealed class SearchNichesQueryHandler : IRequestHandler<SearchNichesQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SearchNichesQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SearchNichesQuery request, CancellationToken cancellationToken)
        {
            QueryBuilder queryBuilder = new();

            // Niches
            var nichesQuery = queryBuilder.BuildQuery<Niche>(request.SearchTerm);
            var niches = await _dbContext.Niches
                .Where(nichesQuery)
                .Select(x => new SearchResult
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = "Niche"
                }).ToListAsync();


            // Subniches
            var subnichesQuery = queryBuilder.BuildQuery<Subniche>(request.SearchTerm);
            var subniches = await _dbContext.Subniches
                .Where(subnichesQuery)
                .Select(x => new SearchResult
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = "Subniche"
                }).ToListAsync();


            // Products
            var productsQuery = queryBuilder.BuildQuery<Product>(request.SearchTerm);
            var products = await _dbContext.Products
                .Where(productsQuery)
                .Select(x => new SearchResult
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = "Product"
                }).ToListAsync();

            List<SearchResult> searchResults = niches
                .Concat(subniches)
                .Concat(products)
                .ToList();

            return Result.Succeeded(searchResults);
        }
    }
}