using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.GetProducts.Queries
{
    public sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetProductsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _dbContext.Products
                .OrderBy(x => x.Name)
                .Where(x => x.SubnicheId == request.ParentId)
                .Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();

            return Result.Succeeded(products);
        }
    }
}