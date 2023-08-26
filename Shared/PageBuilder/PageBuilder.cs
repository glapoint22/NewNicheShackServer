using Shared.Common.Classes;
using Shared.Common.Enums;
using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using Shared.PageBuilder.Classes;
using Shared.PageBuilder.Widgets;
using Shared.PageBuilder.Widgets.GridWidget;
using System.Text.Json;

namespace Shared.PageBuilder
{
    public abstract class PageBuilder
    {
        private readonly IRepository _repository;

        public PageBuilder(IRepository repository)
        {
            _repository = repository;
        }

        // ------------------------------------------------------------------------ Build Page ------------------------------------------------------------------------
        public async Task<PageContent> BuildPage(string pageContent)
        {
            PageContent webPage = Deserialize(pageContent);

            await SetData(webPage);
            return webPage;
        }




        // ------------------------------------------------------------------------ Deserialize -----------------------------------------------------------------------
        protected static PageContent Deserialize(string pageContent)
        {
            return JsonSerializer.Deserialize<PageContent>(pageContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters =
                {
                    new WidgetJsonConverter()
                }
            })!;
        }




        // ------------------------------------------------------------------------- Set Data -------------------------------------------------------------------------
        protected async Task SetData(PageContent page)
        {
            // If the page background has an image
            if (page.Background != null && page.Background.Image != null)
            {
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
                    await SetWidgetData(widget);

                    if (column.WidgetData.WidgetType == WidgetType.Container)
                    {
                        ContainerWidget container = (ContainerWidget)column.WidgetData;

                        if (container.Rows != null && container.Rows.Count > 0) await SetRowData(container.Rows);
                    }
                }
            }
        }





        // ------------------------------------------------------------------------ Get Widget ------------------------------------------------------------------------
        protected static Widget GetWidget(Widget widgetData)
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
                case WidgetType.Niches:
                    widget = (NichesWidget)widgetData;
                    break;
                case WidgetType.Poster:
                    widget = (PosterWidget)widgetData;
                    break;
            }

            return widget;
        }


        // --------------------------------------------------------------------- Set Widget Data ----------------------------------------------------------------------
        protected virtual async Task SetWidgetData(Widget widget)
        {
            await widget.SetData(_repository);
        }
    }
}