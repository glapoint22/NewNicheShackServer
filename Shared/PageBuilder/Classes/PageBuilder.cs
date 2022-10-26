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
        public WebPage BuildPage(string pageContent)
        {
            // Deserialize the content into a page
            WebPage page = JsonSerializer.Deserialize<WebPage>(pageContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters =
                {
                    new WidgetJsonConverter(_repository)
                }
            })!;

            return page;
        }




        // ------------------------------------------------------------------------- Set Data -------------------------------------------------------------------------
        public async Task SetData(WebPage page)
        {
            // If the page background has an image
            if (page.Background != null && page.Background.Image != null)
            {
                //await SetImageData(page.Background.Image);
                await page.Background.Image.SetData(_repository);
            }



            // Rows
            if (page.Rows != null && page.Rows.Count > 0)
            {
                await SetRowData(page.Rows);
            }
        }






        
        // ----------------------------------------------------------------------- Set Row Data -----------------------------------------------------------------------
        private async Task SetRowData(List<Row> rows)
        {
            foreach (Row row in rows)
            {
                if (row.Background != null && row.Background.Image != null)
                {
                    //await SetImageData(row.Background.Image);
                }

                foreach (Column column in row.Columns)
                {
                    if (column.Background != null && column.Background.Image != null)
                    {
                        //await SetImageData(column.Background.Image);
                    }


                    // Create the widget
                    Widget widget = GetWidget(column.WidgetData);



                    // Set the widget data
                    await widget.SetData();

                    if (column.WidgetData.WidgetType == WidgetType.Container)
                    {
                        ContainerWidget container = (ContainerWidget)column.WidgetData;

                        if (container.Rows != null && container.Rows.Count > 0) await SetRowData(container.Rows);
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