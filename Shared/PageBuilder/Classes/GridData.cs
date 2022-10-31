using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Entities;
using Shared.Common.Interfaces;
using System.Linq.Expressions;

namespace Shared.PageBuilder.Classes
{
    public sealed class GridData
    {
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
        public int TotalProducts { get; set; }
        public int PageCount { get; set; }
        public Filters Filters { get; set; } = null!;
        public int ProductCountStart { get; set; }
        public int ProductCountEnd { get; set; }

        private readonly IRepository _repository;
        private const int _limit = 40;

        public GridData(IRepository repository)
        {
            _repository = repository;
        }



        // ------------------------------------------------------------------------ Get Data -----------------------------------------------------------------------------
        public async Task<GridData> GetData(PageParams pageParams)
        {
            await SetData(pageParams);
            return this;
        }





        // ------------------------------------------------------------------------ Set Data -----------------------------------------------------------------------------
        public async Task SetData(PageParams pageParams)
        {
            Products = await GetProducts(pageParams);
            Filters = await GetFilters(pageParams);
            PageCount = (int)Math.Ceiling(Convert.ToDouble(TotalProducts) / _limit);
            ProductCountStart = ((pageParams.Page - 1) * _limit) + 1;
            ProductCountEnd = Math.Min(pageParams.Page * _limit, TotalProducts);
        }




        // ---------------------------------------------------------------------- Get Products ---------------------------------------------------------------------------
        private async Task<List<ProductDto>> GetProducts(PageParams pageParams)
        {
            var query = QueryBuilder.Classes.QueryBuilder.BuildQuery<Product>(pageParams);

            return await _repository.Products
                .Where(query)
                .SortBy(pageParams.SearchTerm, pageParams.SortBy)
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlName = x.UrlName,
                    Rating = x.Rating,
                    TotalReviews = x.TotalReviews,
                    MinPrice = x.ProductPrices.Min(z => z.Price),
                    MaxPrice = x.ProductPrices.Count > 1 ? x.ProductPrices.Max(z => z.Price) : 0,
                    Image = new PageImage
                    {
                        Name = x.Media.Name,
                        Src = x.Media.ImageSm!
                    },
                    OneStar = x.OneStar,
                    TwoStars = x.TwoStars,
                    ThreeStars = x.ThreeStars,
                    FourStars = x.FourStars,
                    FiveStars = x.FiveStars
                })
                .Skip((pageParams.Page - 1) * _limit)
                .Take(_limit)
                .ToListAsync();
        }




        // ---------------------------------------------------------------------- Get Filters ----------------------------------------------------------------------------
        private async Task<Filters> GetFilters(PageParams pageParams)
        {
            List<QueryFilter> customFilters = new();
            List<NicheFilter> nicheFilters = null!;
            PriceFilter priceFilter = null!;
            QueryFilter ratingFilter = null!;

            int length = pageParams.FilterParams.Count == 0 ? 1 : 2;

            for (int i = 0; i < length; i++)
            {
                // If excludeLastFilter is true, query builder will ignore the last filter in the list.
                // This will make sure that all filter options will be shown for that filter and not just
                // the option(s) that are checked
                bool excludeLastFilter = length == 2 && i == 0;


                // Build the query based on the page params
                var query = QueryBuilder.Classes.QueryBuilder.BuildQuery<Product>(pageParams, excludeLastFilter);



                // Get the product data for the filters
                // Product data will also give us the total number of products
                var productData = await _repository.Products
                    .Where(query)
                    .Select(x => new
                    {
                        productId = x.Id,
                        subnicheId = x.SubnicheId,
                        rating = x.Rating,
                        prices = x.ProductPrices
                            .Select(z => z.Price)
                            .ToList()
                    })
                    .ToListAsync();

                // Set the total products
                TotalProducts = productData.Count;

                // Niche Filters
                if (length == 1 || i == 1)
                {
                    nicheFilters = await GetNicheFilters(productData.Select(z => z.subnicheId).ToList());
                }


                // Price Filter
                if (length == 1 ||
                    (pageParams.FilterParams.Last().Name == "Price" && i == 0) ||
                    (pageParams.FilterParams.Last().Name != "Price" && i == 1))
                {
                    priceFilter = await GetPriceFilter(productData.SelectMany(x => x.prices).ToList());
                }



                // Rating Filter
                if (length == 1 ||
                    (pageParams.FilterParams.Last().Name == "Customer Rating" && i == 0) ||
                    (pageParams.FilterParams.Last().Name != "Customer Rating" && i == 1))
                {
                    ratingFilter = GetRatingFilter(productData.Select(x => x.rating).ToList());
                }




                // Custom Filters
                if (length == 1 ||
                    (pageParams.FilterParams.Last().Name != "Price" &&
                        pageParams.FilterParams.Last().Name != "Customer Rating" && i == 0) ||
                    i == 1)
                {
                    // The where expression is used to include or iliminate a filter
                    // This will make sure that we only query for each filter once
                    Expression<Func<ProductFilter, bool>> whereExpression = null!;

                    if (length == 1 ||
                        pageParams.FilterParams.Last().Name == "Price" ||
                        pageParams.FilterParams.Last().Name == "Customer Rating")
                    {
                        // Include all filters
                        whereExpression = x => x != null;
                    }
                    else
                    {
                        string lastFilter = pageParams.FilterParams.Last().Name;

                        if (i == 0)
                        {
                            // Only include the last filter
                            whereExpression = x => x.FilterOption.Filter.Name == lastFilter;
                        }
                        else
                        {
                            // Don't include the last filter
                            whereExpression = x => x.FilterOption.Filter.Name != lastFilter;
                        }
                    }

                    customFilters.AddRange(await GetCustomFilters(productData.Select(x => x.productId).ToList(), whereExpression));
                }


            }

            return new Filters
            {
                NicheFilters = nicheFilters,
                PriceFilter = priceFilter,
                RatingFilter = ratingFilter,
                CustomFilters = customFilters
            };
        }





        // -------------------------------------------------------------------- Get Niche Filters ------------------------------------------------------------------------
        private async Task<List<NicheFilter>> GetNicheFilters(List<int> subnicheIds)
        {
            var subniches = await _repository.Subniches
                .Where(x => subnicheIds.Contains(x.Id))
                .Include(x => x.Niche)
                .ToListAsync();

            return subniches.Select(x => new
            {
                niche = x.Niche,
                subniches = x.Niche.Subniches
            })
            .Distinct()
            .Select(x => new NicheFilter
            {
                Id = x.niche.Id,
                Name = x.niche.Name,
                UrlName = x.niche.UrlName,
                Subniches = x.subniches
                    .Select(z => new SubnicheFilter
                    {
                        Id = z.Id,
                        Name = z.Name,
                        UrlName = z.UrlName
                    })
                    .ToList()
            })
            .ToList();
        }








        // -------------------------------------------------------------------- Get Custom Filters -----------------------------------------------------------------------
        private async Task<List<QueryFilter>> GetCustomFilters(List<int> productIds, Expression<Func<ProductFilter, bool>> whereExpression)
        {
            var filters = await _repository.ProductFilters
                .Where(x => productIds
                    .Contains(x.ProductId))
                .Where(whereExpression)
                .Select(x => new
                {
                    caption = x.FilterOption.Filter.Name,
                    option = new
                    {
                        id = x.FilterOption.Id,
                        label = x.FilterOption.Name
                    }
                })
                .Distinct()
                .ToListAsync();

            return filters
                .GroupBy(x => x.caption, (key, x) => new QueryFilter
                {
                    Caption = key,
                    Options = x.Select(z => new QueryFilterOption
                    {
                        Id = z.option.id,
                        Label = z.option.label
                    }).ToList()
                })
                .ToList();
        }





        // --------------------------------------------------------------------- Get Rating Filter -----------------------------------------------------------------------
        private QueryFilter GetRatingFilter(List<double> ratings)
        {
            List<QueryFilterOption> ratingOptions = new List<QueryFilterOption>();

            // Rating 4 and up
            if (ratings.Count(rating => rating >= 4) > 0)
            {
                ratingOptions.Add(new QueryFilterOption
                {
                    Id = 4
                });
            }



            // Rating 3 and up
            if (ratings.Count(rating => rating >= 3 && rating < 4) > 0)
            {
                ratingOptions.Add(new QueryFilterOption
                {
                    Id = 3
                });
            }



            // Rating 2 and up
            if (ratings.Count(rating => rating >= 2 && rating < 3) > 0)
            {
                ratingOptions.Add(new QueryFilterOption
                {
                    Id = 2
                });
            }




            // Rating 1 and up
            if (ratings.Count(rating => rating >= 1 && rating < 2) > 0)
            {
                ratingOptions.Add(new QueryFilterOption
                {
                    Id = 1
                });
            }


            return new QueryFilter
            {
                Caption = "Customer Rating",
                Options = ratingOptions
            };
        }





        // --------------------------------------------------------------------- Get Price Filter ------------------------------------------------------------------------
        private async Task<PriceFilter> GetPriceFilter(List<double> prices)
        {
            var priceRanges = await _repository.PriceRanges.GetAll();

            var options = priceRanges
                .Where(x => prices
                    .Any(z => z >= x.Min && z <= x.Max))
                .Select(x => new PriceFilterOption
                {
                    Label = x.Label,
                    Min = x.Min,
                    Max = x.Max
                })
                .Distinct()
            .ToList();

            return new PriceFilter
            {
                Caption = "Price",
                Options = options
            };
        }
    }
}