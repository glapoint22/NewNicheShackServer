using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using Shared.PageBuilder.Classes;
using System.Text.Json;

namespace Shared.PageBuilder.Widgets
{
    public sealed class ImageSetWidget : Widget
    {
        public List<LinkableImage>? Images { get; set; }

        public override void SetProperty(string? property, ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            base.SetProperty(property, ref reader, options);

            switch (property)
            {
                case "images":
                    Images = (List<LinkableImage>?)JsonSerializer.Deserialize(ref reader, typeof(List<LinkableImage>), options);
                    break;
            }
        }



        public async override Task SetData(IRepository repository)
        {
            if (Images != null)
            {
                foreach (LinkableImage image in Images)
                {
                    await image.SetData(repository);
                }
            }
        }
    }
}
