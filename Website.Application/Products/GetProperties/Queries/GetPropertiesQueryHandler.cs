using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.Products.GetProperties.Queries
{
    public sealed class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public GetPropertiesQueryHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
        {
            var pricePoints = await _dbContext.PricePoints
                .AsNoTracking()
                .Where(x => x.ProductId == request.ProductId)
                .Select(x => new
                {
                    Image = x.Media != null ? new Image
                    {
                        Name = x.Media.Name,
                        Src = x.Media.ImageSm!
                    } : null,
                    x.Header,
                    x.Quantity,
                    x.UnitPrice,
                    x.Unit,
                    x.StrikethroughPrice,
                    x.ProductPrice.Price,
                    x.ShippingType,
                    x.RecurringPayment
                })
                .ToListAsync();

            var subproducts = await _dbContext.Subproducts
                .Where(x => x.ProductId == request.ProductId)
                .Select(x => new
                {
                    x.Type,
                    x.Name,
                    x.Description,
                    Image = new Image
                    {
                        Name = x.Media.Name,
                        Src = x.Media.ImageSm!
                    },
                    x.Value
                })
                .ToListAsync();

            string subnicheId = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Select(x => x.SubnicheId)
                .SingleAsync();

            var relatedProducts = await _dbContext.Products
                .OrderByDescending(x => x.Rating)
                .Where(x => x.SubnicheId == subnicheId && x.Id != request.ProductId)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.UrlName,
                    x.Rating,
                    x.TotalReviews,
                    MinPrice = x.ProductPrices.MinPrice(),
                    MaxPrice = x.ProductPrices.MaxPrice(),
                    Image = new
                    {
                        x.Media.Name,
                        Src = x.Media.ImageSm!
                    },
                    x.OneStar,
                    x.TwoStars,
                    x.ThreeStars,
                    x.FourStars,
                    x.FiveStars
                })
                .Take(24)
                .ToListAsync();


            return Result.Succeeded(new
            {
                PricePoints = pricePoints,
                Components = subproducts.Where(x => x.Type == 0).ToList(),
                Bonuses = subproducts.Where(x => x.Type == 1).ToList(),
                RelatedProducts = relatedProducts
            });
        }
    }
}