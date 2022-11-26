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
    }
}