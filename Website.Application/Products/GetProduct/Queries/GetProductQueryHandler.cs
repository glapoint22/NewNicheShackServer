using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Entities;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.Products.GetProduct.Queries
{
    public sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IUserService _userService;

        public GetProductQueryHandler(IWebsiteDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();

            Product? product = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.Subproducts)
                    .ThenInclude(x => x.Media)
                .Include(x => x.ProductPrices)
                .Include(x => x.PricePoints)
                    .ThenInclude(x => x.ProductPrice)
                .Include(x => x.PricePoints)
                    .ThenInclude(x => x.Media)
                .Include(x => x.ProductMedia)
                    .ThenInclude(x => x.Media)
                .Include(x => x.Subniche)
                    .ThenInclude(x => x.Niche)
                .SingleOrDefaultAsync();

            if (product == null) return Result.Failed();

            return Result.Succeeded(new
            {
                product.Id,
                product.Name,
                product.UrlName,
                product.Description,
                product.Rating,
                product.TotalReviews,
                MinPrice = product.ProductPrices.Min(z => z.Price),
                MaxPrice = product.ProductPrices.Count > 1 ? product.ProductPrices.Max(z => z.Price) : 0,
                product.OneStar,
                product.TwoStars,
                product.ThreeStars,
                product.FourStars,
                product.FiveStars,
                product.SubnicheId,
                Hoplink = product.Hoplink +
                        (user != null ? (product.Hoplink.Contains('?') ? "&" : "?") + "tid=" +
                        product.TrackingCode + "_" + user.TrackingCode : ""),
                product.ShippingType,
                product.RecurringPayment,
                Components = product.Subproducts
                    .Where(x => x.Type == 0)
                    .Select(x => new
                    {
                        x.Name,
                        x.Description,
                        Image = new Image
                        {
                            Name = x.Media.Name,
                            Src = x.Media.ImageSm!
                        },
                        x.Value
                    }).ToList(),
                Bonuses = product.Subproducts
                    .Where(x => x.Type == 1)
                    .Select(x => new
                    {
                        x.Name,
                        x.Description,
                        Image = new Image
                        {
                            Name = x.Media.Name,
                            Src = x.Media.ImageSm!
                        },
                        x.Value
                    }).ToList(),
                PricePoints = product.PricePoints
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
                    .ToList(),
                Media = product.ProductMedia
                    .OrderBy(x => x.Index)
                    .Select(x => new
                    {
                        x.Media.Name,
                        x.Media.Thumbnail,
                        x.Media.MediaType,
                        x.Media.ImageMd,
                        x.Media.ImageLg,
                        x.Media.VideoId,
                        x.Media.VideoType
                    }),
                Breadcrumb = new List<object>
                {
                    new
                    {
                        product.Subniche.Niche.Name,
                        product.Subniche.Niche.UrlName
                    },
                    new
                    {
                        Id = product.SubnicheId,
                        product.Subniche.Name,
                        product.Subniche.UrlName
                    }
                }
            });
        }
    }
}