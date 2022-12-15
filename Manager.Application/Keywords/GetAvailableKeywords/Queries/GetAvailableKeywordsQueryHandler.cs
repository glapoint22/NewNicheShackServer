using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.GetAvailableKeywords.Queries
{
    public sealed class GetAvailableKeywordsQueryHandler : IRequestHandler<GetAvailableKeywordsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetAvailableKeywordsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetAvailableKeywordsQuery request, CancellationToken cancellationToken)
        {
            var keywords = await _dbContext.KeywordsInKeywordGroup
                .OrderBy(x => x.Keyword.Name)
                .Where(x => x.KeywordGroupId == request.ParenetId)
                .Select(x => new
                {
                    Id = x.KeywordId,
                    x.Keyword.Name
                }).ToListAsync();

            return Result.Succeeded(keywords);
        }
    }
}