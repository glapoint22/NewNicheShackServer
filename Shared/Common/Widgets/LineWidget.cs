using HtmlAgilityPack;
using Shared.Common.Classes;
using System.Text.Json;

namespace Shared.Common.Widgets
{
    public sealed class LineWidget : Widget
    {
        public Border? Border { get; set; }
        public Shadow? Shadow { get; set; }


        public override void SetProperty(string? property, ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            base.SetProperty(property, ref reader, options);

            switch (property)
            {
                case "border":
                    Border = (Border?)JsonSerializer.Deserialize(ref reader, typeof(Border), options);
                    break;

                case "shadow":
                    Shadow = (Shadow?)JsonSerializer.Deserialize(ref reader, typeof(Shadow), options);
                    break;
            }
        }




        public override HtmlNode GenerateHtml(HtmlNode column)
        {
            // Call the base
            HtmlNode widget = base.GenerateHtml(column);


            // Td
            HtmlNode td = widget.SelectSingleNode("tr/td");


            td.SetAttributeValue("style", "border-bottom: " + Border?.Width + "px " + Border?.Style + " " + Border?.Color + ";");
            Shadow?.GenerateHtml(td);


            HtmlNode blankRow = widget.InsertBefore(HtmlNode.CreateNode("<tr>"), widget.SelectSingleNode("tr"));
            HtmlNode blankColumn = blankRow.AppendChild(HtmlNode.CreateNode("<td>"));
            blankColumn.SetAttributeValue("height", "10");


            blankRow = widget.AppendChild(HtmlNode.CreateNode("<tr>"));
            blankColumn = blankRow.AppendChild(HtmlNode.CreateNode("<td>"));
            blankColumn.SetAttributeValue("height", "10");

            return widget;
        }
    }
}