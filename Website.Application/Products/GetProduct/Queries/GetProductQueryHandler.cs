using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

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
            string? userTrackingCode = null;
            User user = await _userService.GetUserFromClaimsAsync();

            if (user != null) userTrackingCode = user.TrackingCode;

            var product = await _dbContext.Products
                .AsNoTracking()
                .Where(x => x.Id == request.ProductId)
                .Select(product => new
                {
                    product.Id,
                    product.Name,
                    product.UrlName,
                    product.Description,
                    product.Rating,
                    product.TotalReviews,
                    MinPrice = product.ProductPrices.MinPrice(),
                    MaxPrice = product.ProductPrices.MaxPrice(),
                    product.OneStar,
                    product.TwoStars,
                    product.ThreeStars,
                    product.FourStars,
                    product.FiveStars,
                    Hoplink = product.GetHoplink(userTrackingCode),
                    product.ShippingType,
                    product.RecurringPayment,
                    Media = product.ProductMedia
                    .OrderBy(x => x.Index)
                    .Select(x => new
                    {
                        x.Media.Name,
                        x.Media.Thumbnail,
                        Type = x.Media.MediaType,
                        x.Media.ImageMd,
                        x.Media.ImageLg,
                        x.Media.VideoId,
                        x.Media.VideoType
                    }),
                    Breadcrumb = new List<object>
                    {
                        new
                        {
                            product.Subniche.Niche.Id,
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
                })
                .SingleOrDefaultAsync();

            if (product == null) return Result.Failed("404");

            return Result.Succeeded(product);
        }
    }
}