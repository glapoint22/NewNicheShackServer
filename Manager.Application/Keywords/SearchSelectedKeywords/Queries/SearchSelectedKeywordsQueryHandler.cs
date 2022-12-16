using Manager.Application.Common.Interfaces;
using Manager.Application.Keywords.Common.Classes;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.QueryBuilder;

namespace Manager.Application.Keywords.SearchSelectedKeywords.Queries
{
    public sealed class SearchSelectedKeywordsQueryHandler : IRequestHandler<SearchSelectedKeywordsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SearchSelectedKeywordsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SearchSelectedKeywordsQuery request, CancellationToken cancellationToken)
        {
            QueryBuilder queryBuilder = new();

            // Keyword Groups
            var keywordGroupsQuery = queryBuilder.BuildQuery<KeywordGroup>(request.SearchTerm);
            var keywordGroups = await _dbContext.KeywordGroups
                .Where(keywordGroupsQuery)
                .Where(x => x.KeywordGroupsBelongingToProduct
                    .Any(z => z.ProductId == request.ProductId))
                .Select(x => new KeywordSearchResult
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = "Group",
                    ForProduct = x.ForProduct
                }).ToListAsync();

            // Keywords
            var keywordsQuery = queryBuilder.BuildQuery<Keyword>(request.SearchTerm);
            var keywords = await _dbContext.Keywords
                .Where(keywordsQuery)
                .Where(x => x.KeywordsInKeywordGroup
                    .Any(z => z.KeywordGroup.KeywordGroupsBelongingToProduct
                        .Any(q => q.ProductId == request.ProductId)))
                .Select(x => new KeywordSearchResult
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = "Keyword",
                    ForProduct = x.KeywordsInKeywordGroup
                        .All(z => z.KeywordGroup.ForProduct),
                    Checked = x.ProductKeywords
                        .Any(z => z.ProductId == request.ProductId)
                }).ToListAsync();

            return Result.Succeeded(keywordGroups.Concat(keywords));
        }
    }
}