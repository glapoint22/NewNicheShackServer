using Manager.Application.Common.Interfaces;
using Manager.Application.Keywords.Common.Classes;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.QueryBuilder;

namespace Manager.Application.Keywords.SearchAvailableKeywords.Queries
{
    public sealed class SearchAvailableKeywordsQueryHandler : IRequestHandler<SearchAvailableKeywordsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SearchAvailableKeywordsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SearchAvailableKeywordsQuery request, CancellationToken cancellationToken)
        {
            QueryBuilder queryBuilder = new();

            // Keyword Groups
            var keywordGroupsQuery = queryBuilder.BuildQuery<KeywordGroup>(request.SearchTerm);
            var keywordGroups = await _dbContext.KeywordGroups
                .Where(keywordGroupsQuery)
                .Where(x => !x.ForProduct)
                .Select(x => new KeywordSearchResult
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = "Group",
                    ForProduct = x.KeywordGroupsBelongingToProduct
                        .Any(z => z.ProductId == request.ProductId)
                }).ToListAsync();

            // Keywords
            var keywordsQuery = queryBuilder.BuildQuery<Keyword>(request.SearchTerm);
            var keywords = await _dbContext.Keywords
                .Where(keywordsQuery)
                .Where(x => x.KeywordsInKeywordGroup
                    .Any(z => !z.KeywordGroup.ForProduct))
                .Select(x => new KeywordSearchResult
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = "Keyword",
                    ForProduct = x.ProductKeywords
                        .Any(z => z.ProductId == request.ProductId)
                }).ToListAsync();

            return Result.Succeeded(keywordGroups.Concat(keywords));
        }
    }
}