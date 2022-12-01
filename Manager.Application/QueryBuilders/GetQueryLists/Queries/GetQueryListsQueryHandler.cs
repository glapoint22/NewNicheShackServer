using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.QueryBuilders.GetQueryLists.Queries
{
    public sealed class GetQueryListsQueryHandler : IRequestHandler<GetQueryListsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetQueryListsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetQueryListsQuery request, CancellationToken cancellationToken)
        {
            var niches = await _dbContext.Niches
                .Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();

            var subniches = await _dbContext.Subniches
                .Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();

            var keywordGroups = await _dbContext.KeywordGroups
                .Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();

            var productGroups = await _dbContext.ProductGroups
                .Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();


            return Result.Succeeded(new
            {
                niches,
                subniches,
                keywordGroups,
                productGroups
            });
        }
    }
}