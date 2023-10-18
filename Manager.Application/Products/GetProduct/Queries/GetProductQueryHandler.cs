using Manager.Application.Common.Classes;
using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Enums;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Products.GetProduct.Queries
{
    public sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result>
    {
        private readonly IManagerDbContext _managerDbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public GetProductQueryHandler(IManagerDbContext managerDbContext, IWebsiteDbContext websiteDbContext)
        {
            _managerDbContext = managerDbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _managerDbContext.Products
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
                    x.Currency,
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
                        x.Vendor.Name,
                        x.Vendor.PrimaryFirstName,
                        x.Vendor.PrimaryLastName,
                        x.Vendor.PrimaryEmail
                    }
                })
                .SingleAsync();

            var pricePoints = await _managerDbContext.PricePoints
                .AsNoTracking()
                .OrderBy(x => x.Id)
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
                    x.Subheader,
                    x.Quantity,
                    x.ProductPrice.Price,
                    x.ShippingType,
                    x.Text,
                    x.Info,
                    x.ShippingValue
                }).ToListAsync();

            var media = await _managerDbContext.ProductMedia
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

            var subproducts = await _managerDbContext.Subproducts
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

                var notificationItems = await _websiteDbContext.Notifications
                    .Where(x => x.ProductId == request.ProductId && x.Type > (int)NotificationType.ReviewComplaint && x.Type < (int)NotificationType.ProductReportedAsIllegal)
                    .Select(x => new
                    {
                        x.Id,
                        x.ProductId,
                        x.NotificationGroupId,
                        NotificationType = x.Type,
                        ProductName = x.Product.Name,
                        Image = x.Product.Media.Thumbnail,
                        IsNew = x.NotificationGroup.ArchiveDate == null ? true: false,
                        Count = x.NotificationGroup.Notifications.Count()
                    }).ToListAsync();


            return Result.Succeeded(new
            {
                product.Id,
                product.Name,
                product.Description,
                product.Hoplink,
                product.ShippingType,
                product.RecurringPayment,
                product.Currency,
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
                    .ToList(),
                notificationItems
            });
        }
    }
}