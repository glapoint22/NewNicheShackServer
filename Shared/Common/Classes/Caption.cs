using HtmlAgilityPack;

namespace Shared.Common.Classes
{
    public sealed class Caption
    {
        public string Text { get; set; } = string.Empty;
        public string FontWeight { get; set; } = string.Empty;
        public string FontStyle { get; set; } = string.Empty;
        public string TextDecoration { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public KeyValuePair<string, string> Font { get; set; }
        public KeyValuePair<string, string> FontSize { get; set; }


        public void GenerateHtml(HtmlNode node)
        {
            string styles = node.GetAttributeValue("style", "");

            // Text
            HtmlTextNode textNode = new HtmlDocument().CreateTextNode(Text);
            node.AppendChild(textNode);



            // Font Weight
            if (FontWeight != null)
            {
                styles += "font-weight: " + FontWeight + ";";
            }


            // Font Style
            if (FontStyle != null)
            {
                styles += "font-style: " + FontStyle + ";";
            }



            // Text Decoration
            if (TextDecoration != null)
            {
                styles += "text-decoration: " + TextDecoration + ";";
            }



            // Color
            styles += "color: " + Color + ";";


            // Font
            styles += "font-family: " + (Font.Value != null ? Font.Value : "Arial, Helvetica, sans-serif") + ";";


            // Font Size
            styles += "font-size: " + (FontSize.Key != null ? FontSize.Key : "14") + "px;";


            // Text Break
            styles += "word-break: break-word;overflow-wrap: break-word;";


            node.SetAttributeValue("style", styles);
        }
    }
}