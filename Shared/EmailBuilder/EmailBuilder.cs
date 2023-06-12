using HtmlAgilityPack;
using Shared.Common.Classes;
using Shared.Common.Enums;
using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using Shared.EmailBuilder.Classes;
using Shared.PageBuilder.Classes;

namespace Shared.EmailBuilder
{
    public sealed class EmailBuilder : PageBuilder.PageBuilder
    {
        public EmailBuilder(IRepository repository) : base(repository) { }

        // ------------------------------------------------------------------------ Build Email -----------------------------------------------------------------------
        public async Task<string> BuildEmail(string content)
        {
            PageContent email = Deserialize(content);

            await SetData(email);
            return CreateBody(email);
        }



        public string CreateBody(PageContent email)
        {
            // Document
            HtmlDocument doc = new();
            HtmlNode node = HtmlNode.CreateNode("<html><head></head><body></body></html>");
            doc.DocumentNode.AppendChild(node);
            node.SelectSingleNode("body").SetAttributeValue("style", "margin: 0;");

            // Meta
            HtmlNode meta = HtmlNode.CreateNode("<meta>");
            meta.SetAttributeValue("name", "viewport");
            meta.SetAttributeValue("content", "width=device-width, initial-scale=1");
            node.FirstChild.AppendChild(meta);


            // Style
            HtmlNode style = HtmlNode.CreateNode("<style>");
            HtmlTextNode styleText = doc.CreateTextNode(
                "a {text-decoration: none}" +
                "body {margin: 0}" +
                "ol, ul {margin-top: 0;margin-bottom: 0;}"
                );
            style.AppendChild(styleText);
            node.FirstChild.AppendChild(style);


            // Main Table
            HtmlNode mainTable = Table.GenerateHtml(doc.DocumentNode.FirstChild.LastChild, new TableOptions
            {
                Background = new Background { Color = "#ffffff" },
                CreateRow = true
            });


            // Set alignment to center
            HtmlNode td = mainTable.SelectSingleNode("tr/td");
            td.SetAttributeValue("align", "center");


            // Body
            HtmlNode body = Table.GenerateHtml(td, new TableOptions
            {
                Width = 600,
                Background = email.Background
            });


            // Rows
            if (email.Rows != null && email.Rows.Count > 0)
            {
                CreateRows(email.Rows, body);
            }


            return doc.DocumentNode.InnerHtml;
        }



        private void CreateRows(List<Row> rows, HtmlNode parent)
        {
            foreach (Row row in rows)
            {
                // Create the row
                HtmlNode tr = row.GenerateHtml(parent);

                foreach (Column column in row.Columns)
                {
                    // Create the column
                    HtmlNode td = column.GenerateHtml(tr);


                    // Create the widget
                    Widget widget = GetWidget(column.WidgetData);
                    HtmlNode widgetNode = widget.GenerateHtml(td);

                    if (column.WidgetData.WidgetType == WidgetType.Container)
                    {
                        ContainerWidget container = (ContainerWidget)column.WidgetData;
                        if (container.Rows != null && container.Rows.Count > 0)
                            CreateRows(container.Rows, widgetNode);
                    }
                }
            }
        }
    }
}