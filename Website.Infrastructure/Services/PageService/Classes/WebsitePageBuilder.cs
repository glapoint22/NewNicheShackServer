using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Dtos;
using Shared.Common.Enums;
using Shared.Common.Widgets;
using Shared.PageBuilder.Classes;
using System.Text.Json;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Infrastructure.Services.PageService.Widgetes;

namespace Website.Infrastructure.Services.PageService.Classes
{
    public sealed class WebsitePageBuilder : PageBuilder
    {
        private readonly IWebsiteDbContext _dbContext;
        private PageParams _pageParams = null!;

        public WebsitePageBuilder(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // ------------------------------------------------------------------------ Build Page ------------------------------------------------------------------------
        public async Task<WebPage> BuildPage(string pageContent, PageParams pageParams)
        {
            _pageParams = pageParams;
            WebPage webPage = GetPage(pageContent);
            await SetData(webPage);

            return webPage;
        }


        // ------------------------------------------------------------------------- Get Page -------------------------------------------------------------------------
        protected override WebPage GetPage(string pageContent)
        {
            return JsonSerializer.Deserialize<WebPage>(pageContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters =
                {
                    new WebsiteWidgetJsonConverter(_dbContext)
                }
            })!;
        }




        // ------------------------------------------------------------------------ Get Widget ------------------------------------------------------------------------
        protected override Widget GetWidget(Widget widgetData)
        {
            Widget widget = null!;

            switch (widgetData.WidgetType)
            {
                case WidgetType.Button:
                    //widget = (ButtonWidget)widgetData;
                    break;
                case WidgetType.Text:
                    //widget = (TextWidget)widgetData;
                    break;
                case WidgetType.Image:
                    //widget = (ImageWidget)widgetData;
                    break;
                case WidgetType.Container:
                    //widget = (ContainerWidget)widgetData;
                    break;
                case WidgetType.Line:
                    //widget = (LineWidget)widgetData;
                    break;
                case WidgetType.Video:
                    //widget = (VideoWidget)widgetData;
                    break;
                case WidgetType.ProductSlider:
                    //widget = (ProductSliderWidget)widgetData;
                    break;
                case WidgetType.Carousel:
                    //widget = (CarouselWidget)widgetData;
                    break;
                case WidgetType.Grid:
                    widget = (GridWidget)widgetData;
                    break;
            }

            return widget;
        }



        // ---------------------------------------------------------------------- Set Image Data ----------------------------------------------------------------------
        protected override async Task SetImageData(PageImage image)
        {
            MediaDto media = await _dbContext.Media
                .Where(x => x.Id == image.Id)
                .Select(x => new MediaDto
                {
                    Name = x.Name,
                    Thumbnail = x.Thumbnail,
                    ImageSm = x.ImageSm,
                    ImageMd = x.ImageMd,
                    ImageLg = x.ImageLg,
                    ImageAnySize = x.ImageAnySize,
                })
                .SingleAsync();

            image.SetData(media);
        }




        // --------------------------------------------------------------------- Set Widget Data ----------------------------------------------------------------------
        protected async override Task SetWidgetData(Widget widget)
        {
            WebsiteWidget? websiteWidget = widget as WebsiteWidget;

            if (websiteWidget != null)
            {
                await websiteWidget.SetData(_pageParams);
            }

        }
    }
}