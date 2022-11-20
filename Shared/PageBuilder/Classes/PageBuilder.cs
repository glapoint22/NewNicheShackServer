using Shared.Common.Classes;
using Shared.Common.Enums;
using Shared.Common.Widgets;

namespace Shared.PageBuilder.Classes
{
    public abstract class PageBuilder
    {
        // ------------------------------------------------------------------------ Build Page ------------------------------------------------------------------------
        public async Task<WebPage> BuildPage(string pageContent)
        {
            WebPage webPage = GetPage(pageContent);
            await SetData(webPage);

            return webPage;
        }




        // ------------------------------------------------------------------------- Set Data -------------------------------------------------------------------------
        protected async Task SetData(WebPage page)
        {
            // If the page background has an image
            if (page.Background != null && page.Background.Image != null)
            {
                await SetImageData(page.Background.Image);
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
                    await SetImageData(row.Background.Image);
                }

                foreach (Column column in row.Columns)
                {
                    if (column.Background != null && column.Background.Image != null)
                    {
                        await SetImageData(column.Background.Image);
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

        protected abstract Widget GetWidget(Widget widgetData);


        // ------------------------------------------------------------------------- Get Page -------------------------------------------------------------------------
        protected abstract WebPage GetPage(string pageContent);


        // ---------------------------------------------------------------------- Set Image Data ----------------------------------------------------------------------
        protected abstract Task SetImageData(PageImage image);

        // --------------------------------------------------------------------- Set Widget Data ----------------------------------------------------------------------
        protected abstract Task SetWidgetData(Widget widget);
    }
}