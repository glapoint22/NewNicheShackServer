using Shared.Common.Classes;
using Shared.Common.Dtos;
using Shared.Common.Widgets;
using Shared.QueryBuilder.Classes;
using System.Text.Json;

namespace Shared.PageBuilder.Widgets
{
    public sealed class ProductSliderWidget : Widget
    {
        public Caption? Caption { get; set; }
        public List<ProductDto>? Products { get; set; }
        public Query? Query { get; set; }


        public override void SetProperty(string? property, ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            base.SetProperty(property, ref reader, options);

            switch (property)
            {
                case "caption":
                    Caption = (Caption?)JsonSerializer.Deserialize(ref reader, typeof(Caption), options);
                    break;
                case "query":
                    Query = (Query?)JsonSerializer.Deserialize(ref reader, typeof(Query), options);
                    break;
            }
        }
    }
}