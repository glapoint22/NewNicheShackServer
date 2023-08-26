using Shared.Common.Classes;
using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using System.Text.Json;

namespace Shared.PageBuilder.Widgets
{
    public sealed class PosterWidget : Widget
    {
        public PageImage? Image { get; set; }
        public PageImage? Image2 { get; set; }
        public Link? Link { get; set; }

        public override void SetProperty(string? property, ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            base.SetProperty(property, ref reader, options);

            switch (property)
            {
                case "image":
                    Image = (PageImage?)JsonSerializer.Deserialize(ref reader, typeof(PageImage), options);
                    break;

                case "image2":
                    Image2 = (PageImage?)JsonSerializer.Deserialize(ref reader, typeof(PageImage), options);
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

            if (Image2 != null)
            {
                await Image2.SetData(repository);
            }

            if (Link != null)
            {
                await Link.SetData(repository);
            }
        }
    }
}