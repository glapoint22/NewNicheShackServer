using HtmlAgilityPack;

namespace Shared.Common.Classes
{
    public class Background
    {
        public string Color { get; set; } = string.Empty;
        public BackgroundImage Image { get; set; } = null!;
        public bool Enabled { get; set; }


        public void GenerateHtml(HtmlNode node)
        {
            string styles = node.GetAttributeValue("style", "");

            // Color
            if (Color != null)
            {
                node.SetAttributeValue("bgcolor", Color);
                styles += "background-color: " + Color + ";";
            }

            // Image
            if (Image != null)
            {
                node.SetAttributeValue("background", "{host}/images/" + Image.Src);
                styles += "background-image: url({host}/images/" + Image.Src + ");";
                Image.GenerateHtml(ref styles);
            }


            node.SetAttributeValue("style", styles);
        }
    }
}