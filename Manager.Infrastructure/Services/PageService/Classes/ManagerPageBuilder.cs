using Manager.Application.Common.Classes;
using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Dtos;
using Shared.Common.Enums;
using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using Shared.PageBuilder;
using Shared.PageBuilder.Widgets;
using Shared.QueryBuilder;

namespace Manager.Infrastructure.Services.PageService.Classes
{
    public sealed class ManagerPageBuilder : PageBuilder
    {
        private readonly IManagerDbContext _dbContext;

        public ManagerPageBuilder(IRepository repository, IManagerDbContext dbContext) : base(repository)
        {
            _dbContext = dbContext;
        }


        // --------------------------------------------------------------------- Set Widget Data ----------------------------------------------------------------------
        protected override async Task SetWidgetData(Widget widget)
        {
            if (widget.WidgetType == WidgetType.ProductSlider)
            {
                var productSliderWidget = (widget as ProductSliderWidget)!;

                if (productSliderWidget.Query != null)
                {
                    var queryBuilder = new QueryBuilder();
                    var query = queryBuilder.BuildQuery<Product>(productSliderWidget.Query);

                    productSliderWidget.Products = await _dbContext.Products
                        .Where(query)
                        .Take(24)
                        .Select(x => new ProductDto
                        {
                            Name = x.Name,
                            MinPrice = x.ProductPrices.MinPrice(),
                            MaxPrice = x.ProductPrices.MaxPrice(),
                            Image = new PageImage
                            {
                                Name = x.Media.Name,
                                Src = x.Media.ImageSm!
                            }
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