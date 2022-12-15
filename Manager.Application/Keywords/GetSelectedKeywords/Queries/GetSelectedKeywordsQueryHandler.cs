using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.GetSelectedKeywords.Queries
{
    public sealed class GetSelectedKeywordsQueryHandler : IRequestHandler<GetSelectedKeywordsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetSelectedKeywordsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetSelectedKeywordsQuery request, CancellationToken cancellationToken)
        {
            var keywords = await _dbContext.KeywordsInKeywordGroup
                .OrderBy(x => x.Keyword.Name)
                .Where(x => x.KeywordGroupId == request.ParentId)
                .Select(x => new
                {
                    Id = x.KeywordId,
                    x.Keyword.Name,
                    Checked = x.Keyword.ProductKeywords.Any(z => z.ProductId == request.ProductId),
                    x.KeywordGroup.ForProduct
                }).ToListAsync();

            return Result.Succeeded(keywords);
        }
    }
}