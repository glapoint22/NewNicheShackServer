using HtmlAgilityPack;
using Shared.Common.Enums;
using Shared.Common.Interfaces;
using Shared.EmailBuilder.Classes;
using System.Text.Json;

namespace Shared.Common.Widgets
{
    public abstract class Widget
    {
        public float? Width { get; set; }
        public float? Height { get; set; }
        public WidgetType WidgetType { get; set; }


        public virtual void SetProperty(string? property, ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            switch (property)
            {
                case "width":
                    Width = (float?)JsonSerializer.Deserialize(ref reader, typeof(float), options);
                    break;

                case "height":
                    Height = (float?)JsonSerializer.Deserialize(ref reader, typeof(float), options);
                    break;
            }
        }


        public virtual Task SetData(IRepository repository)
        {
            return Task.CompletedTask;
        }


        public virtual HtmlNode GenerateHtml(HtmlNode column)
        {
            // Create the table
            HtmlNode table = Table.GenerateHtml(column, new TableOptions
            {
                Width = Width,
                CreateRow = true
            });


            HtmlNode td = table.SelectSingleNode("tr/td");
            td.SetAttributeValue("valign", "top");

            column.AppendChild(new HtmlDocument().CreateComment(Table.MicrosoftIf + "</td></tr></table>" + Table.MicrosoftEndIf));

            return table;
        }
    }
}