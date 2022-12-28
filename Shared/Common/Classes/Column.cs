using HtmlAgilityPack;
using Shared.Common.Widgets;
using Shared.EmailBuilder.Classes;
using Shared.PageBuilder.Classes;

namespace Shared.Common.Classes
{
    public sealed class Column
    {
        public Background Background { get; set; } = null!;
        public Border Border { get; set; } = null!;
        public Corners Corners { get; set; } = null!;
        public Shadow Shadow { get; set; } = null!;
        public Padding Padding { get; set; } = null!;
        public float Width { get; set; }
        public ColumnSpan ColumnSpan { get; set; } = null!;
        public HorizontalAlignment HorizontalAlignment { get; set; } = null!;
        public Widget WidgetData { get; set; } = null!;


        public HtmlNode GenerateHtml(HtmlNode row)
        {
            // Create the column
            HtmlNode column = row.AppendChild(HtmlNode.CreateNode("<td>"));


            column.SetAttributeValue("style", "display: inline-block;width: 100%;max-width: " + Width + "px;");


            // Set the styles
            Background?.SetStyle(column);
            Border?.SetStyle(column);
            Corners?.SetStyle(column);
            Shadow?.SetStyle(column);
            Padding?.SetStyle(column);

            string align = "left";

            if (HorizontalAlignment != null && HorizontalAlignment.Values.Count > 0)
            {
                switch (HorizontalAlignment.Values[0].HorizontalAlignmentType)
                {
                    case 1:
                        align = "center";
                        break;

                    case 2:
                        align = "right";
                        break;

                    default:
                        align = "left";
                        break;
                }
            }


            column.SetAttributeValue("align", align);


            column.AppendChild(new HtmlDocument().CreateComment(Table.MicrosoftIf + "<table width=\"" +
                Width + "\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td>" +
                Table.MicrosoftEndIf));

            return column;
        }
    }
}