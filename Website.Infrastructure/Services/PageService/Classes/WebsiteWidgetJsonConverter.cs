using Shared.Common.Classes;
using Shared.Common.Enums;
using Shared.Common.Widgets;
using System.Text.Json;
using Website.Application.Common.Interfaces;
using Website.Infrastructure.Services.PageService.Widgetes;

namespace Website.Infrastructure.Services.PageService.Classes
{
    public sealed class WebsiteWidgetJsonConverter : WidgetJsonConverter
    {
        private readonly IWebsiteDbContext _dbContext = null!;

        public WebsiteWidgetJsonConverter(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public WebsiteWidgetJsonConverter() { }

        public override Widget GetWidget(WidgetType widgetType)
        {
            Widget widget = null!;

            switch (widgetType)
            {
                case WidgetType.Button:
                    //widget = new ButtonWidget();
                    break;
                case WidgetType.Text:
                    //widget = new TextWidget();
                    break;
                case WidgetType.Image:
                    //widget = new ImageWidget();
                    break;
                case WidgetType.Container:
                    //widget = new ContainerWidget();
                    break;
                case WidgetType.Line:
                    //widget = new LineWidget();
                    break;
                case WidgetType.Video:
                    //widget = new VideoWidget();
                    break;
                case WidgetType.ProductSlider:
                    //widget = new ProductSliderWidget();
                    break;
                case WidgetType.Carousel:
                    //widget = new CarouselWidget();
                    break;
                case WidgetType.Grid:
                    widget = new GridWidget(_dbContext);
                    break;
            }

            return widget;
        }

        public override void SerializeWidget(Utf8JsonWriter writer, Widget widget, JsonSerializerOptions options)
        {
            switch (widget.WidgetType)
            {
                case WidgetType.Button:
                    //JsonSerializer.Serialize(writer, (ButtonWidget)widget, options);
                    break;
                case WidgetType.Text:
                    //JsonSerializer.Serialize(writer, (TextWidget)widget, options);
                    break;
                case WidgetType.Image:
                    //JsonSerializer.Serialize(writer, (ImageWidget)widget, options);
                    break;
                case WidgetType.Container:
                    //JsonSerializer.Serialize(writer, (ContainerWidget)widget, options);
                    break;
                case WidgetType.Line:
                    //JsonSerializer.Serialize(writer, (LineWidget)widget, options);
                    break;
                case WidgetType.Video:
                    //JsonSerializer.Serialize(writer, (VideoWidget)widget, options);
                    break;
                case WidgetType.ProductSlider:
                    //JsonSerializer.Serialize(writer, (ProductSliderWidget)widget, options);
                    break;
                case WidgetType.Carousel:
                    //JsonSerializer.Serialize(writer, (CarouselWidget)widget, options);
                    break;
                case WidgetType.Grid:
                    JsonSerializer.Serialize(writer, (GridWidget)widget, options);
                    break;
            }
        }
    }
}