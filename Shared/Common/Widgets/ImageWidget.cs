using Shared.Common.Classes;
using System.Text.Json;
using Shared.Common.Interfaces;

namespace Shared.Common.Widgets
{
    public sealed class ImageWidget : Widget
    {
        public PageImage? Image { get; set; }
        public Border? Border { get; set; }
        public Corners? Corners { get; set; }
        public Shadow? Shadow { get; set; }
        public Link? Link { get; set; }



        public override void SetProperty(string? property, ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            base.SetProperty(property, ref reader, options);

            switch (property)
            {
                case "image":
                    Image = (PageImage?)JsonSerializer.Deserialize(ref reader, typeof(PageImage), options);
                    break;

                case "border":
                    Border = (Border?)JsonSerializer.Deserialize(ref reader, typeof(Border), options);
                    break;

                case "corners":
                    Corners = (Corners?)JsonSerializer.Deserialize(ref reader, typeof(Corners), options);
                    break;

                case "shadow":
                    Shadow = (Shadow?)JsonSerializer.Deserialize(ref reader, typeof(Shadow), options);
                    break;

                case "link":
                    Link = (Link?)JsonSerializer.Deserialize(ref reader, typeof(Link), options);
                    break;
            }
        }



        public async override Task SetData(IRepository repository)
        {
            if (Image != null)
            {
                await Image.SetData(repository);
            }

            if (Link != null)
            {
                await Link.SetData(repository);
            }
        }
    }
}