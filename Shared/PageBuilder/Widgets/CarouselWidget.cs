using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using Shared.PageBuilder.Classes;
using System.Text.Json;

namespace Shared.PageBuilder.Widgets
{
    public sealed class CarouselWidget : Widget
    {
        public List<LinkableImage>? Banners { get; set; }

        public override void SetProperty(string? property, ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            base.SetProperty(property, ref reader, options);

            switch (property)
            {
                case "banners":
                    Banners = (List<LinkableImage>?)JsonSerializer.Deserialize(ref reader, typeof(List<LinkableImage>), options);
                    break;
            }
        }



        public async override Task SetData(IRepository repository)
        {
            if (Banners != null)
            {
                foreach (LinkableImage banner in Banners)
                {
                    await banner.SetData(repository);
                }
            }
        }
    }
}