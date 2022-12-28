using HtmlAgilityPack;
using Shared.Common.Classes;
using Shared.Common.Interfaces;
using Shared.EmailBuilder.Classes;
using System.Text.Json;

namespace Shared.Common.Widgets
{
    public sealed class ButtonWidget : Widget
    {
        public Background? Background { get; set; }
        public Border? Border { get; set; }
        public Caption? Caption { get; set; }
        public Corners? Corners { get; set; }
        public Shadow? Shadow { get; set; }
        public Padding? Padding { get; set; }
        public Link? Link { get; set; }
        public string? BackgroundHoverColor { get; set; }
        public string? BackgroundActiveColor { get; set; }
        public string? BorderHoverColor { get; set; }
        public string? BorderActiveColor { get; set; }
        public string? TextHoverColor { get; set; }
        public string? TextActiveColor { get; set; }



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

                case "caption":
                    Caption = (Caption?)JsonSerializer.Deserialize(ref reader, typeof(Caption), options);
                    break;

                case "corners":
                    Corners = (Corners?)JsonSerializer.Deserialize(ref reader, typeof(Corners), options);
                    break;

                case "shadow":
                    Shadow = (Shadow?)JsonSerializer.Deserialize(ref reader, typeof(Shadow), options);
                    break;

                case "padding":
                    Padding = (Padding?)JsonSerializer.Deserialize(ref reader, typeof(Padding), options);
                    break;

                case "link":
                    Link = (Link?)JsonSerializer.Deserialize(ref reader, typeof(Link), options);
                    break;
                case "backgroundHoverColor":
                    BackgroundHoverColor = (string?)JsonSerializer.Deserialize(ref reader, typeof(string), options);
                    break;

                case "backgroundActiveColor":
                    BackgroundActiveColor = (string?)JsonSerializer.Deserialize(ref reader, typeof(string), options);
                    break;

                case "borderHoverColor":
                    BorderHoverColor = (string?)JsonSerializer.Deserialize(ref reader, typeof(string), options);
                    break;

                case "borderActiveColor":
                    BorderActiveColor = (string?)JsonSerializer.Deserialize(ref reader, typeof(string), options);
                    break;

                case "textHoverColor":
                    TextHoverColor = (string?)JsonSerializer.Deserialize(ref reader, typeof(string), options);
                    break;

                case "textActiveColor":
                    TextActiveColor = (string?)JsonSerializer.Deserialize(ref reader, typeof(string), options);
                    break;
            }
        }




        public override HtmlNode GenerateHtml(HtmlNode column)
        {
            float height = (float)(Height > 0 ? Height : 40);


            // Call the base
            HtmlNode widget = base.GenerateHtml(column);

            // Td
            HtmlNode td = widget.SelectSingleNode("tr/td");


            // Set the styles
            Background ??= new Background { Color = "#808080" };

            Background.SetStyle(td);
            Border?.SetStyle(td);
            Corners?.SetStyle(td);
            Shadow?.SetStyle(td);

            // Anchor
            HtmlNode anchorNode = HtmlNode.CreateNode("<a>");

            string styles = "display: block;text-align: center;" +
                (Caption?.TextDecoration == null ? "text-decoration: none;" : "");



            var fontSize = Caption?.FontSize.Value != null ? int.Parse(Caption.FontSize.Key) : 14;
            var padding = Math.Max(0, ((height - fontSize) / 2) - 1);

            int paddingTop = Padding != null ? Padding.Values.Where(x => x.PaddingType == 0).Select(x => x.Padding).SingleOrDefault() : 0;
            int paddingRight = Padding != null ? Padding.Values.Where(x => x.PaddingType == 1).Select(x => x.Padding).SingleOrDefault() : 0;
            int paddingBottom = Padding != null ? Padding.Values.Where(x => x.PaddingType == 2).Select(x => x.Padding).SingleOrDefault() : 0;
            int paddingLeft = Padding != null ? Padding.Values.Where(x => x.PaddingType == 3).Select(x => x.Padding).SingleOrDefault() : 0;


            styles += "padding-top: " + (padding + paddingTop) + "px;";
            styles += "padding-bottom: " + (padding + paddingBottom) + "px;";
            styles += "padding-right: " + paddingRight + "px;";
            styles += "padding-left: " + paddingLeft + "px;";


            anchorNode.SetAttributeValue("style", styles);

            // Caption
            Caption?.SetStyle(anchorNode);

            // Link
            Link?.SetStyle(anchorNode);


            td.AppendChild(new HtmlDocument().CreateComment(Table.MicrosoftIf +
                "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"padding-top: " +
                (padding + paddingTop) + "px;padding-bottom: " +
                (padding + paddingBottom) +
                "px;text-align: center;\"><tr><td>" +
                Table.MicrosoftEndIf));



            td.AppendChild(anchorNode);

            td.AppendChild(new HtmlDocument().CreateComment(Table.MicrosoftIf + "</td></tr></table>" + Table.MicrosoftEndIf));

            return anchorNode;
        }





        public async override Task SetData(IRepository repository)
        {
            if (Background != null && Background.Image != null)
            {
                await Background.Image.SetData(repository);
            }

            if (Link != null)
            {
                await Link.SetData(repository);
            }
        }
    }
}