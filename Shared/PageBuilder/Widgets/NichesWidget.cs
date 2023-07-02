using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using Shared.PageBuilder.Classes;
using System.Text.Json;

namespace Shared.PageBuilder.Widgets
{
    public sealed class NichesWidget : Widget
    {
        public List<LinkableImage>? Niches { get; set; }

        public override void SetProperty(string? property, ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            base.SetProperty(property, ref reader, options);

            switch (property)
            {
                case "niches":
                    Niches = (List<LinkableImage>?)JsonSerializer.Deserialize(ref reader, typeof(List<LinkableImage>), options);
                    break;
            }
        }



        public async override Task SetData(IRepository repository)
        {
            if (Niches != null)
            {
                foreach (LinkableImage niche in Niches)
                {
                    await niche.SetData(repository);
                }
            }
        }
    }
}