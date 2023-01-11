using Manager.Application.Common.Interfaces;
using Manager.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Common.Classes;
using Shared.Common.Enums;
using Website.Application.Common.Interfaces;
using IAuthService = Manager.Application.Common.Interfaces.IAuthService;
using IMediaService = Manager.Application.Common.Interfaces.IMediaService;

namespace Manager.Application.Products.PublishProduct.Commands
{
    public sealed class PublishProductCommandHandler : IRequestHandler<PublishProductCommand, Result>
    {
        private readonly IManagerDbContext _managerDbContext;
        private readonly IWebsiteDbContext _websiteDbContext;
        private readonly IMediaService _mediaService;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public PublishProductCommandHandler(IManagerDbContext managerDbContext, IWebsiteDbContext websiteDbContext, IMediaService mediaService, IAuthService authService, IConfiguration configuration)
        {
            _managerDbContext = managerDbContext;
            _websiteDbContext = websiteDbContext;
            _mediaService = mediaService;
            _authService = authService;
            _configuration = configuration;
        }



        // ---------------------------------------------------------------------------------- Handle ------------------------------------------------------------------------------
        public async Task<Result> Handle(PublishProductCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Publish publish = await _managerDbContext.Publishes
                .Where(x => x.ProductId == request.ProductId)
                .SingleAsync();

            if (publish.PublishStatus == PublishStatus.New)
            {
                await PostProduct(request.ProductId);

                _managerDbContext.Publishes.Remove(publish);
                await _managerDbContext.SaveChangesAsync();
            }
            else
            {
                await UpdateProduct(request.ProductId);
            }

            return Result.Succeeded();
        }




        // ----------------------------------------------------------------------------- Publish Filters --------------------------------------------------------------------------
        private async Task PublishFilters(Domain.Entities.Product product)
        {
            // Get the filter ids from website that this product is using
            List<Guid> websiteFilterIds = await _websiteDbContext.Filters
                .Where(x => product.ProductFilters
                    .Select(z => z.FilterOption.FilterId)
                    .Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync();

            // Get the filter ids from manager that website does not have
            List<Guid> managerFilterIds = product.ProductFilters
                .Where(x => !websiteFilterIds
                    .Contains(x.FilterOption.FilterId))
                .Select(x => x.FilterOption.FilterId)
                .ToList();

            // If we have filter ids from manager that website does not have
            if (managerFilterIds.Count > 0)
            {
                // Get the filters from manager and add it to website
                List<Website.Domain.Entities.Filter> filters = await _managerDbContext.Filters
                .Where(x => managerFilterIds.Contains(x.Id))
                .Select(x => new Website.Domain.Entities.Filter
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

                _websiteDbContext.Filters.AddRange(filters);
            }


            // Get the filter option ids from website that this product is using
            List<Guid> websiteFilterOptionIds = await _websiteDbContext.FilterOptions
                .Where(x => product.ProductFilters
                    .Select(z => z.FilterOptionId)
                    .Contains(x.Id))
                .Select(z => z.Id)
                .ToListAsync();


            // Get the filter option ids from manager that website does not have
            List<Guid> managerFilterOptionIds = product.ProductFilters
                .Where(x => !websiteFilterOptionIds
                    .Contains(x.FilterOptionId))
                .Select(x => x.FilterOptionId)
                .ToList();


            // If we have filter option ids from manager that website does not have
            if (managerFilterOptionIds.Count > 0)
            {
                // Get the filter options from manager and add it to website
                List<Website.Domain.Entities.FilterOption> filterOptions = await _managerDbContext.FilterOptions
                .Where(x => managerFilterOptionIds.Contains(x.Id))
                .Select(x => new Website.Domain.Entities.FilterOption
                {
                    Id = x.Id,
                    FilterId = x.FilterId,
                    Name = x.Name
                }).ToListAsync();

                _websiteDbContext.FilterOptions.AddRange(filterOptions);
            }


            // Add the product filters to website
            List<Website.Domain.Entities.ProductFilter> productFilters = product.ProductFilters
                .Select(x => new Website.Domain.Entities.ProductFilter
                {
                    ProductId = x.ProductId,
                    FilterOptionId = x.FilterOptionId
                }).ToList();

            _websiteDbContext.ProductFilters.AddRange(productFilters);
        }






        // ---------------------------------------------------------------------------- Publish Keywords --------------------------------------------------------------------------
        private async Task PublishKeywords(Domain.Entities.Product product)
        {
            // Get the keyword ids from website that this product is using
            List<Guid> websiteKeywordIds = await _websiteDbContext.Keywords
                .Where(x => product.ProductKeywords
                    .Select(z => z.KeywordId)
                    .Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync();



            // Get the keyword ids from manager that website does not have
            List<Guid> managerKeywordIds = product.ProductKeywords
                .Where(x => !websiteKeywordIds
                    .Contains(x.KeywordId))
                .Select(x => x.KeywordId)
                .ToList();



            // If we have keyword ids from manager that website does not have
            if (managerKeywordIds.Count > 0)
            {
                // Get the keywords from manager and add it to website
                List<Website.Domain.Entities.Keyword> keywords = await _managerDbContext.Keywords
                .Where(x => managerKeywordIds.Contains(x.Id))
                .Select(x => new Website.Domain.Entities.Keyword
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

                _websiteDbContext.Keywords.AddRange(keywords);
            }



            // Add the product keywords to website
            List<Website.Domain.Entities.ProductKeyword> productKeywords = product.ProductKeywords
                .Select(x => new Website.Domain.Entities.ProductKeyword
                {
                    ProductId = x.ProductId,
                    KeywordId = x.KeywordId
                }).ToList();

            _websiteDbContext.ProductKeywords.AddRange(productKeywords);
        }









        // ------------------------------------------------------------------------------ Publish Media ---------------------------------------------------------------------------
        private async Task PublishMedia(Domain.Entities.Product product)
        {
            // Get the media ids from website that this product is using
            List<Guid> websiteMediaIds = await _websiteDbContext.Media
                .Where(x => product.ProductMedia
                    .Select(z => z.MediaId)
                    .Contains(x.Id) ||
                    product.PricePoints
                        .Select(z => z.ImageId)
                        .Contains(x.Id) ||
                    product.Subproducts
                        .Select(z => z.ImageId)
                        .Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync();



            // Get the media ids from manager that website does not have
            List<Guid> managerMediaIds = product.ProductMedia
                .Where(x => !websiteMediaIds
                    .Contains((Guid)x.MediaId!))
                .Select(x => (Guid)x.MediaId!)
                .ToList();

            List<Guid> pricePointImageids = product.PricePoints
                 .Where(x => !websiteMediaIds
                     .Contains((Guid)x.ImageId!))
                 .Select(x => (Guid)x.ImageId!)
                 .ToList();

            List<Guid> subproductImageIds = product.Subproducts
                 .Where(x => !websiteMediaIds
                     .Contains((Guid)x.ImageId!))
                 .Select(x => (Guid)x.ImageId!)
                 .ToList();

            managerMediaIds = managerMediaIds
                .Concat(pricePointImageids)
                .Concat(subproductImageIds)
                .ToList();




            // If we have media ids from manager that website does not have
            if (managerMediaIds.Count > 0)
            {
                // Get media from manager and add it to website
                List<Website.Domain.Entities.Media> media = await _managerDbContext.Media
                .Where(x => managerMediaIds.Contains(x.Id))
                .Select(x => new Website.Domain.Entities.Media
                {
                    Id = x.Id,
                    Name = x.Name,
                    Thumbnail = x.Thumbnail,
                    ImageSm = x.ImageSm,
                    ImageMd = x.ImageMd,
                    ImageLg = x.ImageLg,
                    ImageAnySize = x.ImageAnySize,
                    VideoId = x.VideoId,
                    MediaType = x.MediaType,
                    VideoType = x.VideoType
                }).ToListAsync();

                _websiteDbContext.Media.AddRange(media);


                List<string> images = new();

                foreach (var m in media)
                {
                    if (m.Thumbnail != null && !images.Contains(m.Thumbnail)) images.Add(m.Thumbnail);
                    if (m.ImageSm != null && !images.Contains(m.ImageSm)) images.Add(m.ImageSm);
                    if (m.ImageMd != null && !images.Contains(m.ImageMd)) images.Add(m.ImageMd);
                    if (m.ImageLg != null && !images.Contains(m.ImageLg)) images.Add(m.ImageLg);
                    if (m.ImageAnySize != null && !images.Contains(m.ImageAnySize)) images.Add(m.ImageAnySize);
                }

                HttpResponseMessage result = await _mediaService.PostImages(images, _configuration["Website:Images"], _authService.GetAccessTokenFromHeader()!);
            }






            // Add the product media to website
            List<Website.Domain.Entities.ProductMedia> productMedia = product.ProductMedia
                .Select(x => new Website.Domain.Entities.ProductMedia
                {
                    ProductId = x.ProductId,
                    MediaId = (Guid)x.MediaId!,
                    Index = x.Index
                }).ToList();

            _websiteDbContext.ProductMedia.AddRange(productMedia);
        }














        // ----------------------------------------------------------------------------- Publish Prices ---------------------------------------------------------------------------
        private void PublishPrices(Domain.Entities.Product product)
        {
            List<Website.Domain.Entities.ProductPrice> productPrices = product.ProductPrices
                .Select(x => new Website.Domain.Entities.ProductPrice
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Price = x.Price,
                }).ToList();

            _websiteDbContext.ProductPrices.AddRange(productPrices);
        }















        // ------------------------------------------------------------------------------- Post Product ---------------------------------------------------------------------------
        private async Task PostProduct(Guid productId)
        {
            // Get the product from manager
            Domain.Entities.Product product = await _managerDbContext.Products
                .AsSplitQuery()
                .Where(x => x.Id == productId)
                .Include(x => x.Subniche)
                    .ThenInclude(x => x.Niche)
                .Include(x => x.ProductFilters)
                    .ThenInclude(x => x.FilterOption)
                .Include(x => x.ProductKeywords)
                .Include(x => x.ProductMedia)
                .Include(x => x.ProductPrices)
                .Include(x => x.ProductsInProductGroup)
                .Include(x => x.PricePoints)
                .Include(x => x.Subproducts)
                .SingleAsync();


            // Media
            await PublishMedia(product);

            // Niche
            await PublishNiche(product);

            // Subniche
            await PublishSubniche(product);

            // Product
            PuslishProduct(product);

            // Filters
            await PublishFilters(product);

            // Keywords
            await PublishKeywords(product);

            // Prices
            PublishPrices(product);

            // Products in product group
            PublishProductsInProductGroup(product);

            // Price Points
            PublishPricePoints(product);

            // Subproducts
            PuslishSubproducts(product);

            await _websiteDbContext.SaveChangesAsync();
        }







        // ------------------------------------------------------------------------------ Publish Niche ---------------------------------------------------------------------------
        private async Task PublishNiche(Domain.Entities.Product product)
        {
            if (!await _websiteDbContext.Niches
                .AnyAsync(x => x.Id == product.Subniche.NicheId))
            {
                Website.Domain.Entities.Niche niche = new()
                {
                    Id = product.Subniche.NicheId,
                    Name = product.Subniche.Niche.Name,
                    UrlName = product.Subniche.Niche.UrlName
                };

                _websiteDbContext.Niches.Add(niche);
            }
        }










        // ------------------------------------------------------------------- Publish Products In Product Group ------------------------------------------------------------------
        private void PublishProductsInProductGroup(Domain.Entities.Product product)
        {
            List<Website.Domain.Entities.ProductInProductGroup> productsInProductGroup = product.ProductsInProductGroup
                .Select(x => new Website.Domain.Entities.ProductInProductGroup
                {
                    ProductId = x.ProductId,
                    ProductGroupId = x.ProductGroupId
                }).ToList();

            _websiteDbContext.ProductsInProductGroup.AddRange(productsInProductGroup);
        }










        // -------------------------------------------------------------------------- Publish Price Points ------------------------------------------------------------------------
        private void PublishPricePoints(Domain.Entities.Product product)
        {
            List<Website.Domain.Entities.PricePoint> pricePoints = product.PricePoints
                .Select(x => new Website.Domain.Entities.PricePoint
                {
                    Id = x.Id,
                    ProductPriceId = x.ProductPriceId,
                    ProductId = x.ProductId,
                    ImageId = x.ImageId,
                    Header = x.Header,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    Unit = x.Unit,
                    StrikethroughPrice = x.StrikethroughPrice,
                    ShippingType = x.ShippingType,
                    RecurringPayment = x.RecurringPayment
                }).ToList();

            _websiteDbContext.PricePoints.AddRange(pricePoints);
        }










        // ---------------------------------------------------------------------------- Puslish Product ---------------------------------------------------------------------------
        private void PuslishProduct(Domain.Entities.Product product)
        {
            Website.Domain.Entities.Product websiteProduct = new()
            {
                Id = product.Id,
                SubnicheId = product.SubnicheId,
                ImageId = (Guid)product.ImageId!,
                Name = product.Name,
                UrlName = product.UrlName,
                Description = product.Description!,
                Hoplink = product.Hoplink!,
                ShippingType = product.ShippingType,
                RecurringPayment = product.RecurringPayment,
                Date = product.Date,
                Disabled = product.Disabled
            };

            _websiteDbContext.Products.Add(websiteProduct);
        }










        // ---------------------------------------------------------------------------- Publish Subniche --------------------------------------------------------------------------
        private async Task PublishSubniche(Domain.Entities.Product product)
        {
            if (!await _websiteDbContext.Subniches
                .AnyAsync(x => x.Id == product.SubnicheId))
            {
                Website.Domain.Entities.Subniche subniche = new()
                {
                    Id = product.SubnicheId,
                    NicheId = product.Subniche.NicheId,
                    Name = product.Subniche.Name,
                    UrlName = product.Subniche.UrlName
                };

                _websiteDbContext.Subniches.Add(subniche);
            }
        }














        // -------------------------------------------------------------------------- Puslish Subproducts -------------------------------------------------------------------------
        private void PuslishSubproducts(Domain.Entities.Product product)
        {
            List<Website.Domain.Entities.Subproduct> subproducts = product.Subproducts
                .Select(x => new Website.Domain.Entities.Subproduct
                {
                    Id = x.Id,
                    Name = x.Name!,
                    Description = x.Description!,
                    ProductId = x.ProductId,
                    ImageId = (Guid)x.ImageId!,
                    Value = x.Value,
                    Type = x.Type
                }).ToList();

            _websiteDbContext.Subproducts.AddRange(subproducts);
        }








        private Task UpdateProduct(Guid productId)
        {
            throw new NotImplementedException();
        }


    }
}