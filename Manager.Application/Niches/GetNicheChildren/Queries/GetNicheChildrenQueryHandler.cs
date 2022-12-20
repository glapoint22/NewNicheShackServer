using Manager.Application.Common.Classes;
using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Niches.GetNicheChildren.Queries
{
    public sealed class GetNicheChildrenQueryHandler : IRequestHandler<GetNicheChildrenQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetNicheChildrenQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetNicheChildrenQuery request, CancellationToken cancellationToken)
        {
            var subniches = await _dbContext.Subniches
                .Where(x => x.NicheId == request.ParentId)
                .Select(x => new SearchResult
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

            var products = await _dbContext.Products
                .Where(x => subniches
                    .Select(z => z.Id)
                    .Contains(x.SubnicheId))
                .Select(x => new SearchResult
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();

            var children = subniches.Concat(products).ToList();

            return Result.Succeeded(children);
        }
    }
}