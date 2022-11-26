using Shared.Common.Classes;
using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using Shared.PageBuilder.Classes;
using System.Text.Json;

namespace Shared.PageBuilder.Widgets
{
    public sealed class VideoWidget : Widget
    {
        public Border? Border { get; set; }
        public Corners? Corners { get; set; }
        public Shadow? Shadow { get; set; }
        public Video? Video { get; set; }



        public override void SetProperty(string? property, ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            base.SetProperty(property, ref reader, options);


            switch (property)
            {
                case "border":
                    Border = (Border?)JsonSerializer.Deserialize(ref reader, typeof(Border), options);
                    break;

                case "corners":
                    Corners = (Corners?)JsonSerializer.Deserialize(ref reader, typeof(Corners), options);
                    break;

                case "shadow":
                    Shadow = (Shadow?)JsonSerializer.Deserialize(ref reader, typeof(Shadow), options);
                    break;

                case "video":
                    Video = (Video?)JsonSerializer.Deserialize(ref reader, typeof(Video), options);
                    break;
            }
        }



        public async override Task SetData(IRepository repository)
        {
            if (Video != null)
            {
                await Video.SetData(repository);
            }
        }
    }
}