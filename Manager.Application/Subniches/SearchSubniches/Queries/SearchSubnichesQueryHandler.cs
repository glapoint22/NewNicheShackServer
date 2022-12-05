using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.QueryBuilder;

namespace Manager.Application.Subniches.SearchSubniches.Queries
{
    public sealed class SearchSubnichesQueryHandler : IRequestHandler<SearchSubnichesQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SearchSubnichesQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SearchSubnichesQuery request, CancellationToken cancellationToken)
        {
            QueryBuilder queryBuilder = new QueryBuilder();

            var query = queryBuilder.BuildQuery<Subniche>(request.SearchTerm);
            var subniches = await _dbContext.Subniches
                .Where(query)
                .Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();

            return Result.Succeeded(subniches);
        }
    }
}