using Manager.Application._Publish.Common.Classes;
using Manager.Application.Common.Interfaces;
using Manager.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application._Publish.PublishProduct.Commands
{
    public sealed class PublishProductCommandHandler : Publish, IRequestHandler<PublishProductCommand, Result>
    {
        public PublishProductCommandHandler(
            IWebsiteDbContext websiteDbContext,
            IManagerDbContext managerDbContext,
            Application.Common.Interfaces.IMediaService mediaService,
            Application.Common.Interfaces.IAuthService authService,
            IConfiguration configuration) : base(websiteDbContext, managerDbContext, mediaService, authService, configuration)
        {
        }





        // ---------------------------------------------------------------------------------- Handle ------------------------------------------------------------------------------
        public async Task<Result> Handle(PublishProductCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.PublishItem? publishItem = await _managerDbContext.PublishItems
                .Where(x => x.ProductId == request.Product.Id)
                .SingleOrDefaultAsync();

            if (publishItem != null)
            {
                if (publishItem.PublishStatus == PublishStatus.New)
                {
                    await PostProduct(request.Product);
                }
                else
                {
                    await SetProduct(request.Product);
                }

                // Remove the publish item
                _managerDbContext.PublishItems.Remove(publishItem);
                await _managerDbContext.SaveChangesAsync();
            }

            return Result.Succeeded();
        }









        // ------------------------------------------------------------------------------- Post Product ---------------------------------------------------------------------------
        private async Task PostProduct(Domain.Entities.Product product)
        {
            // Media
            await PublishMedia(product);

            // Niche
            await PublishNiche(product);

            // Subniche
            await PublishSubniche(product);

            // Filters
            await PublishFilters(product);

            // Keywords
            await PublishKeywords(product);

            // Product
            PublishProduct(product);

            // Product Prices
            PublishProductPrices(product);

            // Product groups
            PublishProductGroups(product);

            // Price Points
            PublishPricePoints(product);

            // Subproducts
            PuslishSubproducts(product);

            // Product Media
            PublishProductMedia(product);

            // Product Filters
            PublishProductFilters(product);

            // Product Keywords
            PublishProductKeywords(product);

            // Save
            await _websiteDbContext.SaveChangesAsync();
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
                await PostImages(managerMediaIds);
            }
        }








        // ------------------------------------------------------------------------- Publish Product Prices -----------------------------------------------------------------------
        private void PublishProductPrices(Domain.Entities.Product product)
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







        // ------------------------------------------------------------------------ Publish Product Filters -----------------------------------------------------------------------
        private void PublishProductFilters(Domain.Entities.Product product)
        {
            List<Website.Domain.Entities.ProductFilter> productFilters = product.ProductFilters
                .Select(x => new Website.Domain.Entities.ProductFilter
                {
                    ProductId = x.ProductId,
                    FilterOptionId = x.FilterOptionId
                }).ToList();

            _websiteDbContext.ProductFilters.AddRange(productFilters);
        }











        // ------------------------------------------------------------------------ Publish Product Keywords -----------------------------------------------------------------------
        private void PublishProductKeywords(Domain.Entities.Product product)
        {
            List<Website.Domain.Entities.ProductKeyword> productKeywords = product.ProductKeywords
                .Select(x => new Website.Domain.Entities.ProductKeyword
                {
                    ProductId = x.ProductId,
                    KeywordId = x.KeywordId
                }).ToList();

            _websiteDbContext.ProductKeywords.AddRange(productKeywords);
        }











        // ------------------------------------------------------------------------- Publish Product Media ------------------------------------------------------------------------
        private void PublishProductMedia(Domain.Entities.Product product)
        {
            List<Website.Domain.Entities.ProductMedia> productMedia = product.ProductMedia
                .Select(x => new Website.Domain.Entities.ProductMedia
                {
                    ProductId = x.ProductId,
                    MediaId = (Guid)x.MediaId!,
                    Index = x.Index
                }).ToList();

            _websiteDbContext.ProductMedia.AddRange(productMedia);
        }












        // ------------------------------------------------------------------------- Publish Product Groups -----------------------------------------------------------------------
        private void PublishProductGroups(Domain.Entities.Product product)
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










        // ---------------------------------------------------------------------------- Publish Product ---------------------------------------------------------------------------
        private void PublishProduct(Domain.Entities.Product product)
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
            };

            _websiteDbContext.Products.Add(websiteProduct);
        }










        // ----------------------------------------------------------------------------- Update Product ---------------------------------------------------------------------------
        private async Task UpdateProduct(Domain.Entities.Product product)
        {
            Website.Domain.Entities.Product websiteProduct = await _websiteDbContext.Products
                .Where(x => x.Id == product.Id)
                .Include(x => x.ProductPrices)
                .SingleAsync();

            websiteProduct.ImageId = (Guid)product.ImageId!;
            websiteProduct.Name = product.Name;
            websiteProduct.UrlName = product.UrlName;
            websiteProduct.Description = product.Description!;
            websiteProduct.Hoplink = product.Hoplink!;
            websiteProduct.ShippingType = product.ShippingType;
            websiteProduct.RecurringPayment = product.RecurringPayment;

            foreach (var productPrice in product.ProductPrices)
            {
                var websiteProductPrice = websiteProduct.ProductPrices
                    .Where(x => x.Id == productPrice.Id)
                    .SingleOrDefault();

                if (websiteProductPrice != null)
                {
                    websiteProductPrice.Price = productPrice.Price;
                }
            }
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







        // ------------------------------------------------------------------------------- Set Product ----------------------------------------------------------------------------
        private async Task SetProduct(Domain.Entities.Product product)
        {
            await PublishMedia(product);

            // Niche
            await PublishNiche(product);

            // Subniche
            await PublishSubniche(product);

            // Filters
            await PublishFilters(product);

            // Keywords
            await PublishKeywords(product);

            // Product Filters
            await UpdateProductFilters(product);

            // Product Keywords
            await UpdateProductKeywords(product);

            // Product groups
            await UpdateProductGroups(product);

            // Product media
            await UpdateProductMedia(product);

            // Price points
            await UpdatePricePoints(product);

            // Subproducts
            await UpdateSubproducts(product);

            // Product
            await UpdateProduct(product);

            await _websiteDbContext.SaveChangesAsync();
        }






        // ------------------------------------------------------------------------- Update Product Filters -----------------------------------------------------------------------
        private async Task UpdateProductFilters(Domain.Entities.Product product)
        {
            // Get manager filter option ids that this product is using
            List<Guid> managerFilterOptionIds = product.ProductFilters
                .Select(x => x.FilterOptionId)
                .ToList();

            // Get website product filters that this product is using
            List<Website.Domain.Entities.ProductFilter> websiteProductFilters = await _websiteDbContext.ProductFilters
                .Where(x => x.ProductId == product.Id)
                .ToListAsync();


            // Get filter option ids that website does not have
            List<Guid> filterOptionIds = managerFilterOptionIds
                .Where(x => !websiteProductFilters
                    .Select(z => z.FilterOptionId)
                    .Contains(x))
                .ToList();


            // If we have product filters that we need to add to website
            if (filterOptionIds.Count > 0)
            {
                foreach (Guid filterOptionId in filterOptionIds)
                {
                    _websiteDbContext.ProductFilters.Add(new Website.Domain.Entities.ProductFilter
                    {
                        ProductId = product.Id,
                        FilterOptionId = filterOptionId
                    });
                }
            }

            // Get product filters that manager does not have
            List<Website.Domain.Entities.ProductFilter> productFilters = websiteProductFilters
                .Where(x => !managerFilterOptionIds
                    .Contains(x.FilterOptionId))
                .ToList();



            // If we have product filters that we need to remove from website
            if (productFilters.Count > 0)
            {
                foreach (Website.Domain.Entities.ProductFilter productFilter in productFilters)
                {
                    _websiteDbContext.ProductFilters.Remove(productFilter);
                }
            }
        }









        // ------------------------------------------------------------------------- Update Product Keywords -----------------------------------------------------------------------
        private async Task UpdateProductKeywords(Domain.Entities.Product product)
        {
            // Get manager keyword ids that this product is using
            List<Guid> managerKeywordIds = product.ProductKeywords
                .Select(x => x.KeywordId)
                .ToList();

            // Get website product keywords that this product is using
            List<Website.Domain.Entities.ProductKeyword> websiteProductKeywords = await _websiteDbContext.ProductKeywords
                .Where(x => x.ProductId == product.Id)
                .ToListAsync();


            // Get keyword ids that website does not have
            List<Guid> keywordIds = managerKeywordIds
                .Where(x => !websiteProductKeywords
                    .Select(z => z.KeywordId)
                    .Contains(x))
                .ToList();


            // If we have product keywords that we need to add to website
            if (keywordIds.Count > 0)
            {
                foreach (Guid keywordId in keywordIds)
                {
                    _websiteDbContext.ProductKeywords.Add(new Website.Domain.Entities.ProductKeyword
                    {
                        ProductId = product.Id,
                        KeywordId = keywordId
                    });
                }
            }

            // Get product keywords that manager does not have
            List<Website.Domain.Entities.ProductKeyword> productKeywords = websiteProductKeywords
                .Where(x => !managerKeywordIds
                    .Contains(x.KeywordId))
                .ToList();



            // If we have product keywords that we need to remove from website
            if (productKeywords.Count > 0)
            {
                foreach (Website.Domain.Entities.ProductKeyword productKeyword in productKeywords)
                {
                    _websiteDbContext.ProductKeywords.Remove(productKeyword);
                }
            }
        }









        // -------------------------------------------------------------------------- Update Product Groups ------------------------------------------------------------------------
        private async Task UpdateProductGroups(Domain.Entities.Product product)
        {
            // Get manager product group ids that this product is using
            List<Guid> managerProductGroupIds = product.ProductsInProductGroup
                .Select(x => x.ProductGroupId)
                .ToList();

            // Get website products in product Group that this product is using
            List<Website.Domain.Entities.ProductInProductGroup> websiteProductsInProductGroup = await _websiteDbContext.ProductsInProductGroup
                .Where(x => x.ProductId == product.Id)
                .ToListAsync();


            // Get product group ids that website does not have
            List<Guid> productGroupIds = managerProductGroupIds
                .Where(x => !websiteProductsInProductGroup
                    .Select(z => z.ProductGroupId)
                    .Contains(x))
                .ToList();


            // If we have products in product group that we need to add to website
            if (productGroupIds.Count > 0)
            {
                foreach (Guid productGroupId in productGroupIds)
                {
                    _websiteDbContext.ProductsInProductGroup.Add(new Website.Domain.Entities.ProductInProductGroup
                    {
                        ProductId = product.Id,
                        ProductGroupId = productGroupId
                    });
                }
            }

            // Get products in product group that manager does not have
            List<Website.Domain.Entities.ProductInProductGroup> productsInProductGroup = websiteProductsInProductGroup
                .Where(x => !managerProductGroupIds
                    .Contains(x.ProductGroupId))
                .ToList();



            // If we have products in product group that we need to remove from website
            if (productsInProductGroup.Count > 0)
            {
                foreach (Website.Domain.Entities.ProductInProductGroup productInProductGroup in productsInProductGroup)
                {
                    _websiteDbContext.ProductsInProductGroup.Remove(productInProductGroup);
                }
            }
        }










        // --------------------------------------------------------------------------- Update Product Media ------------------------------------------------------------------------
        private async Task UpdateProductMedia(Domain.Entities.Product product)
        {
            // Get manager media ids that this product is using
            List<Guid> managerMediaIds = product.ProductMedia
                .Select(x => (Guid)x.MediaId!)
                .ToList();

            // Get website product media that this product is using
            List<Website.Domain.Entities.ProductMedia> websiteProductMedia = await _websiteDbContext.ProductMedia
                .Where(x => x.ProductId == product.Id)
                .ToListAsync();


            // Get product media that website does not have
            List<Domain.Entities.ProductMedia> managerProductMedia = product.ProductMedia
                .Where(x => !websiteProductMedia
                    .Select(z => z.MediaId)
                    .Contains((Guid)x.MediaId!))
                .ToList();


            // If we have product media that we need to add to website
            if (managerProductMedia.Count > 0)
            {
                foreach (var pm in managerProductMedia)
                {
                    _websiteDbContext.ProductMedia.Add(new Website.Domain.Entities.ProductMedia
                    {
                        ProductId = product.Id,
                        MediaId = (Guid)pm.MediaId!,
                        Index = pm.Index
                    });
                }
            }

            // Get product media that manager does not have
            List<Website.Domain.Entities.ProductMedia> productMedia = websiteProductMedia
                .Where(x => !managerMediaIds
                    .Contains(x.MediaId))
                .ToList();



            // If we have product media that we need to remove from website
            if (productMedia.Count > 0)
            {
                foreach (Website.Domain.Entities.ProductMedia pm in productMedia)
                {
                    _websiteDbContext.ProductMedia.Remove(pm);
                }
            }


            // Sync up the indices
            foreach (var pm in product.ProductMedia)
            {
                Website.Domain.Entities.ProductMedia? websiteProdMedia = websiteProductMedia
                    .Where(x => x.MediaId == pm.MediaId)
                    .SingleOrDefault();

                if (websiteProdMedia != null)
                {
                    websiteProdMedia.Index = pm.Index;
                }
            }
        }










        // ---------------------------------------------------------------------------- Update Price Points ------------------------------------------------------------------------
        private async Task UpdatePricePoints(Domain.Entities.Product product)
        {
            // Get manager price point ids that this product is using
            List<Guid> managerPricePointIds = product.PricePoints
                .Select(x => x.Id)
                .ToList();

            // Get website price points that this product is using
            List<Website.Domain.Entities.PricePoint> websitePricePoints = await _websiteDbContext.PricePoints
                .Where(x => x.ProductId == product.Id)
                .Include(x => x.ProductPrice)
                .ToListAsync();


            // Get price points that website does not have
            List<Domain.Entities.PricePoint> managerPricePoints = product.PricePoints
                .Where(x => !websitePricePoints
                    .Select(z => z.Id)
                    .Contains(x.Id))
                .ToList();


            // If we have price points that we need to add to website
            if (managerPricePoints.Count > 0)
            {
                foreach (var pp in managerPricePoints)
                {
                    _websiteDbContext.PricePoints.Add(new Website.Domain.Entities.PricePoint
                    {
                        Id = pp.Id,
                        ProductPriceId = pp.ProductPriceId,
                        ProductId = product.Id,
                        ImageId = pp.ImageId,
                        Header = pp.Header,
                        Quantity = pp.Quantity,
                        UnitPrice = pp.UnitPrice,
                        Unit = pp.Unit,
                        StrikethroughPrice = pp.StrikethroughPrice,
                        ShippingType = pp.ShippingType,
                        RecurringPayment = pp.RecurringPayment,
                        ProductPrice = new Website.Domain.Entities.ProductPrice
                        {
                            Id = pp.ProductPrice.Id,
                            ProductId = pp.ProductPrice.ProductId,
                            Price = pp.ProductPrice.Price
                        }
                    });
                }
            }

            // Get price points that manager does not have
            List<Website.Domain.Entities.PricePoint> pricePoints = websitePricePoints
                .Where(x => !managerPricePointIds
                    .Contains(x.Id))
                .ToList();



            // If we have price points that we need to remove from website
            if (pricePoints.Count > 0)
            {
                foreach (Website.Domain.Entities.PricePoint pp in pricePoints)
                {
                    _websiteDbContext.PricePoints.Remove(pp);
                    _websiteDbContext.ProductPrices.Remove(pp.ProductPrice);
                }

                if (managerPricePointIds.Count == 0)
                {
                    _websiteDbContext.ProductPrices.Add(new Website.Domain.Entities.ProductPrice
                    {
                        Id = product.ProductPrices[0].Id,
                        ProductId = product.Id,
                        Price = product.ProductPrices[0].Price
                    });
                }
            }


            // Update any changes
            foreach (var pp in product.PricePoints)
            {
                Website.Domain.Entities.PricePoint? websitePP = websitePricePoints
                    .Where(x => x.Id == pp.Id)
                    .SingleOrDefault();

                if (websitePP != null)
                {
                    websitePP.ProductPriceId = pp.ProductPriceId;
                    websitePP.ProductId = product.Id;
                    websitePP.ImageId = pp.ImageId;
                    websitePP.Header = pp.Header;
                    websitePP.Quantity = pp.Quantity;
                    websitePP.UnitPrice = pp.UnitPrice;
                    websitePP.Unit = pp.Unit;
                    websitePP.StrikethroughPrice = pp.StrikethroughPrice;
                    websitePP.ShippingType = pp.ShippingType;
                    websitePP.RecurringPayment = pp.RecurringPayment;
                    websitePP.ProductPrice.Price = pp.ProductPrice.Price;
                }
            }
        }










        // ---------------------------------------------------------------------------- Update Subproducts -------------------------------------------------------------------------
        private async Task UpdateSubproducts(Domain.Entities.Product product)
        {
            // Get manager subproduct ids that this product is using
            List<Guid> managerSubproductIds = product.Subproducts
                .Select(x => x.Id)
                .ToList();

            // Get website subproducts that this product is using
            List<Website.Domain.Entities.Subproduct> websiteSubproducts = await _websiteDbContext.Subproducts
                .Where(x => x.ProductId == product.Id)
                .ToListAsync();


            // Get subproducts that website does not have
            List<Domain.Entities.Subproduct> managerSubproducts = product.Subproducts
                .Where(x => !websiteSubproducts
                    .Select(z => z.Id)
                    .Contains(x.Id))
                .ToList();


            // If we have Subproducts that we need to add to website
            if (managerSubproducts.Count > 0)
            {
                foreach (var sp in managerSubproducts)
                {
                    _websiteDbContext.Subproducts.Add(new Website.Domain.Entities.Subproduct
                    {
                        Id = sp.Id,
                        Name = sp.Name!,
                        Description = sp.Description!,
                        ProductId = product.Id,
                        ImageId = (Guid)sp.ImageId!,
                        Value = sp.Value,
                        Type = sp.Type
                    });
                }
            }

            // Get subproducts that manager does not have
            List<Website.Domain.Entities.Subproduct> subproducts = websiteSubproducts
                .Where(x => !managerSubproductIds
                    .Contains(x.Id))
                .ToList();



            // If we have subproducts that we need to remove from website
            if (subproducts.Count > 0)
            {
                foreach (Website.Domain.Entities.Subproduct sp in subproducts)
                {
                    _websiteDbContext.Subproducts.Remove(sp);
                }
            }


            // Update the subproducts
            foreach (var sp in product.Subproducts)
            {
                Website.Domain.Entities.Subproduct? websiteSubProds = websiteSubproducts
                    .Where(x => x.Id == sp.Id)
                    .SingleOrDefault();

                if (websiteSubProds != null)
                {
                    websiteSubProds.Name = sp.Name!;
                    websiteSubProds.Description = sp.Description!;
                    websiteSubProds.ProductId = product.Id;
                    websiteSubProds.ImageId = (Guid)sp.ImageId!;
                    websiteSubProds.Value = sp.Value;
                    websiteSubProds.Type = sp.Type;
                }
            }
        }
    }
}