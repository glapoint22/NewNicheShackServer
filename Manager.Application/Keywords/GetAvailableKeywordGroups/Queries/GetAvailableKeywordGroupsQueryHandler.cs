using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.GetAvailableKeywordGroups.Queries
{
    public sealed class GetAvailableKeywordGroupsQueryHandler : IRequestHandler<GetAvailableKeywordGroupsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetAvailableKeywordGroupsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetAvailableKeywordGroupsQuery request, CancellationToken cancellationToken)
        {
            var keywordGroups = await _dbContext.KeywordGroups
                .OrderBy(x => x.Name)
                .Where(x => !x.ForProduct)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    ForProduct = x.KeywordGroupsBelongingToProduct
                        .Any(z => z.ProductId == request.ProductId)
                }).ToListAsync();

            return Result.Succeeded(keywordGroups);
        }
    }
}