using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.QueryBuilder;

namespace Manager.Application.ProductGroups.SearchProductGroups.Queries
{
    public sealed class SearchProductGroupsQueryHandler : IRequestHandler<SearchProductGroupsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SearchProductGroupsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SearchProductGroupsQuery request, CancellationToken cancellationToken)
        {
            QueryBuilder queryBuilder = new();

            var query = queryBuilder.BuildQuery<ProductGroup>(request.SearchTerm);
            var productGroups = await _dbContext.ProductGroups
                .Where(query)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    Checked = x.ProductsInProductGroup
                        .Any(z => z.ProductId == request.ProductId)
                }).ToListAsync();

            return Result.Succeeded(productGroups);
        }
    }
}