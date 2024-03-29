﻿using HtmlAgilityPack;

namespace Shared.EmailBuilder.Classes
{
    public sealed class Table
    {
        public static string MicrosoftIf = "<!--[if (mso)|(gte mso 9)|(IE)|(mso 16)]>";
        public static string MicrosoftEndIf = "<![endif]-->";

        public static HtmlNode GenerateHtml(HtmlNode parent)
        {
            HtmlNode table = parent.AppendChild(HtmlNode.CreateNode("<table>"));
            table.SetAttributeValue("width", "100%");
            table.SetAttributeValue("cellPadding", "0");
            table.SetAttributeValue("cellSpacing", "0");
            table.SetAttributeValue("border", "0");

            table.SetAttributeValue("width", "100%");
            table.SetAttributeValue("style", "width: 100%;");

            return table;
        }



        public static HtmlNode GenerateHtml(HtmlNode parent, TableOptions tableOptions)
        {
            HtmlNode table = parent.AppendChild(HtmlNode.CreateNode("<table>"));
            table.SetAttributeValue("width", "100%");
            table.SetAttributeValue("cellPadding", "0");
            table.SetAttributeValue("cellSpacing", "0");
            table.SetAttributeValue("border", "0");

            // Horizontal Alignment
            if (tableOptions.HorizontalAlignment != null)
            {
                table.SetAttributeValue("align", tableOptions.HorizontalAlignment);
            }

            // Background
            if (tableOptions.Background != null)
            {
                tableOptions.Background.GenerateHtml(table);
            }

            string styles = table.GetAttributeValue("style", "");


            // Row
            if (tableOptions.CreateRow)
            {
                HtmlNode tr = HtmlNode.CreateNode("<tr>");
                HtmlNode td = HtmlNode.CreateNode("<td>");

                tr.AppendChild(td);
                table.AppendChild(tr);
            }


            // Width
            if (tableOptions.Width != null && tableOptions.Width > 0)
            {
                // Color
                string bgColor = tableOptions.Background != null && tableOptions.Background.Color != null ? " bgcolor=\"" + tableOptions.Background.Color + "\"" : string.Empty;

                // Image
                string image = tableOptions.Background != null && tableOptions.Background.Image != null ? " background=\"{host}/images/" + tableOptions.Background.Image.Src + "\"" : string.Empty;

                string align = tableOptions.HorizontalAlignment != null ? "align=\"" + tableOptions.HorizontalAlignment + "\"" : string.Empty;

                parent.InsertBefore(new HtmlDocument().CreateComment(MicrosoftIf + "<table width=\"" + tableOptions.Width + "\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"" + bgColor + image + align + "><tr><td>" + MicrosoftEndIf), table);
                parent.AppendChild(new HtmlDocument().CreateComment(MicrosoftIf + "</td></tr></table>" + MicrosoftEndIf));


                styles += "width: 100%;max-width: " + tableOptions.Width + "px;";
            }
            else
            {
                styles += "width: 100%;";
                table.SetAttributeValue("width", "100%");
            }

            // Set the styles
            table.SetAttributeValue("style", styles);

            return table;
        }
    }
}