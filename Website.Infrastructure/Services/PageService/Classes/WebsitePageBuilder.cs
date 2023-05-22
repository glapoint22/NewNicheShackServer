using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Dtos;
using Shared.Common.Enums;
using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using Shared.PageBuilder;
using Shared.PageBuilder.Widgets;
using Shared.PageBuilder.Widgets.GridWidget;
using Shared.PageBuilder.Widgets.GridWidget.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Infrastructure.Services.PageService.Classes
{
    public sealed class WebsitePageBuilder : PageBuilder
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly PageParams _pageParams = null!;

        public WebsitePageBuilder(IWebsiteDbContext dbContext, IRepository repository) : base(repository)
        {
            _dbContext = dbContext;
        }


        public WebsitePageBuilder(IWebsiteDbContext dbContext, IRepository repository, PageParams pageParams) : base(repository)
        {
            _dbContext = dbContext;
            _pageParams = pageParams;
        }




        // --------------------------------------------------------------------- Set Widget Data ----------------------------------------------------------------------
        protected override async Task SetWidgetData(Widget widget)
        {
            if (widget.WidgetType == WidgetType.Grid)
            {
                var gridWidget = (widget as GridWidget)!;
                WebsiteGridData gridData = new(_dbContext);
                gridWidget.GridData = await gridData.GetData(_pageParams);
            }
            else if (widget.WidgetType == WidgetType.ProductSlider)
            {
                var productSliderWidget = (widget as ProductSliderWidget)!;

                if (productSliderWidget.Query != null)
                {
                    var queryBuilder = new WebsiteQueryBuilder();
                    var query = queryBuilder.BuildQuery<Product>(productSliderWidget.Query);

                    productSliderWidget.Products = await _dbContext.Products
                        .Where(query)
                        .Where(x => !x.Disabled)
                        .Take(24)
                        .Select(x => new ProductDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                            UrlName = x.UrlName,
                            Rating = x.Rating,
                            TotalReviews = x.TotalReviews,
                            MinPrice = x.ProductPrices.MinPrice(),
                            MaxPrice = x.ProductPrices.MaxPrice(),
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
                        .ToListAsync();
                }
            }
            else
            {
                await base.SetWidgetData(widget);
            }
        }
    }
}