using Manager.Application.Common.Classes;
using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Niches.GetSubnichesProducts.Queries
{
    public sealed class GetSubnichesProductsQueryHandler : IRequestHandler<GetSubnichesProductsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetSubnichesProductsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetSubnichesProductsQuery request, CancellationToken cancellationToken)
        {
            var subniches = await _dbContext.Subniches
                .Where(x => x.NicheId == request.NicheId)
                .Select(x => new SearchResult
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

            var products = await _dbContext.Products
                .Where(x => x.SubnicheId == request.SubnicheId)
                .Select(x => new SearchResult
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();

            return Result.Succeeded(new
            {
                subniches,
                products
            });
        }
    }
}