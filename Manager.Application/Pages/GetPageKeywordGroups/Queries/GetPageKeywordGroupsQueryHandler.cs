using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Pages.GetPageKeywordGroups.Queries
{
    public sealed class GetPageKeywordGroupsQueryHandler : IRequestHandler<GetPageKeywordGroupsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetPageKeywordGroupsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetPageKeywordGroupsQuery request, CancellationToken cancellationToken)
        {
            var keywordGroupIds = await _dbContext.PageKeywordGroups
                .Where(x => x.PageId == request.PageId)
                .Select(x => x.KeywordGroupId)
            .ToListAsync();

            var keywordGroups = await _dbContext.KeywordGroups
                .Where(x => keywordGroupIds.Contains(x.Id))
                .Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();

            return Result.Succeeded(keywordGroups);
        }
    }
}