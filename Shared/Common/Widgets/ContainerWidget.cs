using HtmlAgilityPack;
using Shared.Common.Classes;
using Shared.Common.Interfaces;
using Shared.EmailBuilder.Classes;
using System.Text.Json;

namespace Shared.Common.Widgets
{
    public sealed class ContainerWidget : Widget
    {
        public Background? Background { get; set; }
        public Border? Border { get; set; }
        public Corners? Corners { get; set; }
        public Shadow? Shadow { get; set; }
        public List<Row>? Rows { get; set; }

        public override void SetProperty(string? property, ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            base.SetProperty(property, ref reader, options);


            switch (property)
            {
                case "background":
                    Background = (Background?)JsonSerializer.Deserialize(ref reader, typeof(Background), options);
                    break;

                case "border":
                    Border = (Border?)JsonSerializer.Deserialize(ref reader, typeof(Border), options);
                    break;

                case "corners":
                    Corners = (Corners?)JsonSerializer.Deserialize(ref reader, typeof(Corners), options);
                    break;

                case "shadow":
                    Shadow = (Shadow?)JsonSerializer.Deserialize(ref reader, typeof(Shadow), options);
                    break;


                case "rows":
                    Rows = (List<Row>?)JsonSerializer.Deserialize(ref reader, typeof(List<Row>), options);
                    break;
            }
        }


        public async override Task SetData(IRepository repository)
        {
            if (Background != null && Background.Image != null)
            {
                await Background.Image.SetData(repository);
            }
        }




        public override HtmlNode GenerateHtml(HtmlNode column)
        {
            // Call the base
            HtmlNode widget = base.GenerateHtml(column);

            // Td
            HtmlNode td = widget.SelectSingleNode("tr/td");
            td.SetAttributeValue("height", Height.ToString());
            td.SetAttributeValue("style", "height: " + Height + "px;");

            // Set the styles
            Background?.GenerateHtml(td);
            Border?.GenerateHtml(td);
            Corners?.GenerateHtml(td);
            Shadow?.GenerateHtml(td);

            HtmlNode container = Table.GenerateHtml(td);

            return container;
        }
    }
}