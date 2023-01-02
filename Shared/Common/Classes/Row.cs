using HtmlAgilityPack;

namespace Shared.Common.Classes
{
    public sealed class Row
    {
        public float Top { get; set; }
        public Background Background { get; set; } = null!;
        public Border Border { get; set; } = null!;
        public Corners Corners { get; set; } = null!;
        public Shadow Shadow { get; set; } = null!;
        public Padding Padding { get; set; } = null!;
        public VerticalAlignment VerticalAlignment { get; set; } = null!;
        public List<Column> Columns { get; set; } = null!;
        public float RelativeTop { get; set; }


        public HtmlNode GenerateHtml(HtmlNode table)
        {
            // Insert a row for spacing
            if (RelativeTop > 0)
            {
                HtmlNode blankRow = table.AppendChild(HtmlNode.CreateNode("<tr>"));
                HtmlNode blankColumn = blankRow.AppendChild(HtmlNode.CreateNode("<td>"));
                blankColumn.SetAttributeValue("height", RelativeTop.ToString());
            }

            // Create the row
            HtmlNode row = table.AppendChild(HtmlNode.CreateNode("<tr>"));

            // Set the styles
            Background?.GenerateHtml(row);
            Border?.GenerateHtml(row);
            Corners?.GenerateHtml(row);
            Shadow?.GenerateHtml(row);
            Padding?.GenerateHtml(row);

            string valign = "top";

            if (VerticalAlignment != null && VerticalAlignment.Values.Count > 0)
            {
                switch (VerticalAlignment.Values[0].VerticalAlignmentType)
                {
                    case 1:
                        valign = "middle";
                        break;

                    case 2:
                        valign = "bottom";
                        break;

                    default:
                        valign = "top";
                        break;
                }
            }

            row.SetAttributeValue("valign", valign);
            row.SetAttributeValue("align", "center");

            return row;
        }
    }
}