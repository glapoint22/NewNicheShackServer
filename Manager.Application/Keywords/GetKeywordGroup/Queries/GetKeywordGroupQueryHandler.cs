using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.GetKeywordGroup.Queries
{
    public sealed class GetKeywordGroupQueryHandler : IRequestHandler<GetKeywordGroupQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetKeywordGroupQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetKeywordGroupQuery request, CancellationToken cancellationToken)
        {
            var keywordGroup = await _dbContext.KeywordsInKeywordGroup
                .Where(x => x.KeywordId == request.KeywordId)
                .Select(x => new
                {
                    Id = x.KeywordGroupId,
                    x.KeywordGroup.Name
                }).FirstAsync();

            return Result.Succeeded(keywordGroup);
        }
    }
}