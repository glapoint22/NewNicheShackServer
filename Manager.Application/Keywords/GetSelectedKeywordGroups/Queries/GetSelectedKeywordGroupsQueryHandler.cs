using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.GetSelectedKeywordGroups.Queries
{
    public sealed class GetSelectedKeywordGroupsQueryHandler : IRequestHandler<GetSelectedKeywordGroupsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetSelectedKeywordGroupsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetSelectedKeywordGroupsQuery request, CancellationToken cancellationToken)
        {
            var keywordGroups = await _dbContext.KeywordGroups
                .OrderBy(x => x.Name)
                .Where(x => x.KeywordGroupsBelongingToProduct
                    .Any(z => z.ProductId == request.ProductId))
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.ForProduct
                })
                .ToListAsync();

            return Result.Succeeded(keywordGroups);
        }
    }
}