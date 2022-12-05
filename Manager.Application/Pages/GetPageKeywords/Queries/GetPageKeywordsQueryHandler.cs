using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Pages.GetPageKeywords.Queries
{
    public sealed class GetPageKeywordsQueryHandler : IRequestHandler<GetPageKeywordsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetPageKeywordsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetPageKeywordsQuery request, CancellationToken cancellationToken)
        {
            var keywords = await _dbContext.KeywordsInKeywordGroup
                .Where(x => x.KeywordGroupId == request.KeywordGroupId)
                .Select(x => new
                {
                    Id = x.KeywordId,
                    x.Keyword.Name,
                    Checked = x.PageKeywords
                        .Any(z => z.PageId == request.PageId && z.KeywordInKeywordGroupId == x.Id)
                })
                .ToListAsync();

            return Result.Succeeded(keywords);
        }
    }
}