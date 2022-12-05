using Manager.Application.Common.Classes;
using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.QueryBuilder;

namespace Manager.Application.Keywords.SearchKeywordGroups.Queries
{
    public sealed class SearchKeywordGroupsQueryHandler : IRequestHandler<SearchKeywordGroupsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SearchKeywordGroupsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SearchKeywordGroupsQuery request, CancellationToken cancellationToken)
        {
            QueryBuilder queryBuilder = new QueryBuilder();

            var query = queryBuilder.BuildQuery<KeywordGroup>(request.SearchTerm);
            var keywordGroups = await _dbContext.KeywordGroups
                .Where(query)
                .Where(x => !x.ForProduct)
                .Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();

            return Result.Succeeded(keywordGroups);
        }
    }
}