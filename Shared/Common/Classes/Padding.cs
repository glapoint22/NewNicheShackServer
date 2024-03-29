﻿using HtmlAgilityPack;
using Shared.Common.Enums;

namespace Shared.Common.Classes
{
    public sealed class Padding
    {
        public List<PaddingValue> Values { get; set; } = new List<PaddingValue>();

        public void GenerateHtml(HtmlNode node)
        {
            string styles = node.GetAttributeValue("style", "");

            styles += "padding-top: " + GetValue(PaddingType.Top) + "px;";
            styles += "padding-right: " + GetValue(PaddingType.Right) + "px;";
            styles += "padding-bottom: " + GetValue(PaddingType.Bottom) + "px;";
            styles += "padding-left: " + GetValue(PaddingType.Left) + "px;";

            node.SetAttributeValue("style", styles);
        }

        public int GetValue(PaddingType paddingType)
        {
            return Values
                .Where(x => x.PaddingType == (int)paddingType)
                .Select(x => x.Padding)
                .SingleOrDefault();
        }
    }
}