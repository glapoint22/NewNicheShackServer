using Shared.Common.Classes;
using Shared.Common.Enums;
using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using Shared.PageBuilder.Widgets;
using System.Text.Json;

namespace Shared.PageBuilder.Classes
{
    public sealed class PageBuilder
    {
        private readonly IRepository _repository;

        public PageBuilder(IRepository repository)
        {
            _repository = repository;
        }

        // ------------------------------------------------------------------------ Build Page ------------------------------------------------------------------------
        public async Task<WebPage> BuildPage(string pageContent)
        {
            WebPage webPage = GetPage(pageContent);
            await SetData(webPage);

            return webPage;
        }





        // ------------------------------------------------------------------------ Build Page ------------------------------------------------------------------------
        public async Task<WebPage> BuildPage(string pageContent, PageParams pageParams)
        {
            WebPage webPage = GetPage(pageContent);
            await SetData(webPage, pageParams);

            return webPage;
        }



        // ------------------------------------------------------------------------- Get Page -------------------------------------------------------------------------
        private WebPage GetPage(string pageContent)
        {
            return JsonSerializer.Deserialize<WebPage>(pageContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters =
                {
                    new WidgetJsonConverter(_repository)
                }
            })!;
        }



        // ------------------------------------------------------------------------- Set Data -------------------------------------------------------------------------
        private async Task SetData(WebPage page, PageParams? pageParams = null)
        {
            // If the page background has an image
            if (page.Background != null && page.Background.Image != null)
            {
                await page.Background.Image.SetData(_repository);
            }



            // Rows
            if (page.Rows != null && page.Rows.Count > 0)
            {
                await SetRowData(page.Rows, pageParams);
            }
        }







        // ----------------------------------------------------------------------- Set Row Data -----------------------------------------------------------------------
        private async Task SetRowData(List<Row> rows, PageParams? pageParams)
        {
            foreach (Row row in rows)
            {
                if (row.Background != null && row.Background.Image != null)
                {
                    await row.Background.Image.SetData(_repository);
                }

                foreach (Column column in row.Columns)
                {
                    if (column.Background != null && column.Background.Image != null)
                    {
                        await column.Background.Image.SetData(_repository);
                    }


                    // Create the widget
                    Widget widget = GetWidget(column.WidgetData);


                    // Set the widget data
                    await widget.SetData(pageParams);

                    if (column.WidgetData.WidgetType == WidgetType.Container)
                    {
                        ContainerWidget container = (ContainerWidget)column.WidgetData;

                        if (container.Rows != null && container.Rows.Count > 0) await SetRowData(container.Rows, pageParams);
                    }
                }
            }
        }





        // ------------------------------------------------------------------------ Get Widget ------------------------------------------------------------------------

        private static Widget GetWidget(Widget widgetData)
        {
            Widget widget = null!;

            switch (widgetData.WidgetType)
            {
                case WidgetType.Button:
                    widget = (ButtonWidget)widgetData;
                    break;
                case WidgetType.Text:
                    widget = (TextWidget)widgetData;
                    break;
                case WidgetType.Image:
                    widget = (ImageWidget)widgetData;
                    break;
                case WidgetType.Container:
                    widget = (ContainerWidget)widgetData;
                    break;
                case WidgetType.Line:
                    widget = (LineWidget)widgetData;
                    break;
                case WidgetType.Video:
                    widget = (VideoWidget)widgetData;
                    break;
                case WidgetType.ProductSlider:
                    widget = (ProductSliderWidget)widgetData;
                    break;
                case WidgetType.Carousel:
                    widget = (CarouselWidget)widgetData;
                    break;
                case WidgetType.Grid:
                    widget = (GridWidget)widgetData;
                    break;
            }

            return widget;
        }
    }
}