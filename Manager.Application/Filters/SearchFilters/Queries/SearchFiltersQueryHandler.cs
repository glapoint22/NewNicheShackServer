using Manager.Application.Common.Interfaces;
using Manager.Application.Filters.SearchFilters.Classes;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.QueryBuilder;

namespace Manager.Application.Filters.SearchFilters.Queries
{
    public sealed class SearchFiltersQueryHandler : IRequestHandler<SearchFiltersQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SearchFiltersQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SearchFiltersQuery request, CancellationToken cancellationToken)
        {
            QueryBuilder queryBuilder = new();

            // Filters
            var filtersQuery = queryBuilder.BuildQuery<Filter>(request.SearchTerm);
            var filters = await _dbContext.Filters
                .Where(filtersQuery)
                .Select(x => new FilterSearchResult
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = "Filter"
                }).ToListAsync();

            // Filter Options
            var filterOptionsQuery = queryBuilder.BuildQuery<FilterOption>(request.SearchTerm);
            var filterOptions = await _dbContext.FilterOptions
                .Where(filterOptionsQuery)
                .Select(x => new FilterSearchResult
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = "Option",
                    Checked = x.ProductFilters
                        .Any(z => z.ProductId == request.ProductId)
                }).ToListAsync();

            List<FilterSearchResult> searchResults = filters
                .Concat(filterOptions)
                .ToList();

            return Result.Succeeded(searchResults);
        }
    }
}