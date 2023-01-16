using Manager.Application.Common.Classes;
using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.GetProduct.Queries
{
    public sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetProductQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .AsNoTracking()
                .Where(x => x.Id == request.ProductId)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Description,
                    x.Hoplink,
                    x.ShippingType,
                    x.RecurringPayment,
                    MinPrice = x.ProductPrices.Count > 0 ? x.ProductPrices.MinPrice() : null,
                    MaxPrice = x.ProductPrices.MaxPrice(),
                    Niche = new
                    {
                        Id = x.Subniche.NicheId,
                        x.Subniche.Niche.Name
                    },
                    Subniche = new
                    {
                        Id = x.SubnicheId,
                        x.Subniche.Name
                    },
                    Vendor = new
                    {
                        Id = x.VendorId,
                        x.Vendor.Name
                    }
                })
                .SingleAsync();

            var pricePoints = await _dbContext.PricePoints
                .AsNoTracking()
                .Where(x => x.ProductId == request.ProductId)
                .Select(x => new
                {
                    x.Id,
                    Image = new
                    {
                        Id = x.ImageId,
                        x.Media.Name,
                        Src = x.Media.ImageSm
                    },
                    x.Header,
                    x.Quantity,
                    x.UnitPrice,
                    x.Unit,
                    x.StrikethroughPrice,
                    x.ProductPrice.Price,
                    x.ShippingType,
                    x.RecurringPayment
                }).ToListAsync();

            var media = await _dbContext.ProductMedia
                .Where(x => x.ProductId == request.ProductId)
                .OrderBy(x => x.Index)
                .Select(x => new
                {
                    ProductMediaId = x.Id,
                    x.Media.Id,
                    x.Media.Name,
                    x.Media.Thumbnail,
                    Type = x.Media.MediaType,
                    x.Media.ImageMd,
                    x.Media.VideoId,
                    x.Media.VideoType,
                    x.Index
                }).ToListAsync();

            var subproducts = await _dbContext.Subproducts
                .Where(x => x.ProductId == request.ProductId)
                .Select(x => new
                {
                    x.Id,
                    x.Type,
                    x.Name,
                    x.Description,
                    Image = new
                    {
                        id = x.ImageId,
                        x.Media.Name,
                        Src = x.Media.ImageSm
                    },
                    x.Value
                }).ToListAsync();


            return Result.Succeeded(new
            {
                product.Id,
                product.Name,
                product.Description,
                product.Hoplink,
                product.ShippingType,
                product.RecurringPayment,
                product.MinPrice,
                product.MaxPrice,
                product.Niche,
                product.Subniche,
                product.Vendor,
                media,
                pricePoints,
                Components = subproducts
                    .Where(x => x.Type == 0)
                    .ToList(),
                Bonuses = subproducts
                    .Where(x => x.Type == 1)
                    .ToList()
            });
        }
    }
}