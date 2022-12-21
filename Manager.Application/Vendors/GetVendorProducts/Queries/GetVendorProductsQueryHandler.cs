using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Vendors.GetVendorProducts.Queries
{
    public sealed class GetVendorProductsQueryHandler : IRequestHandler<GetVendorProductsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetVendorProductsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetVendorProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _dbContext.Products
                .Where(x => x.VendorId == request.Id)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    Image = x.Media.Thumbnail
                }).ToListAsync();

            return Result.Succeeded(products);
        }
    }
}