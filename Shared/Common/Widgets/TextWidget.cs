using Shared.Common.Classes;
using Shared.Common.Interfaces;
using System.Text.Json;

namespace Shared.Common.Widgets
{
    public sealed class TextWidget : Widget
    {
        public Background? Background { get; set; }
        public Padding? Padding { get; set; }
        public List<TextBoxData>? TextBoxData { get; set; }



        public override void SetProperty(string? property, ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            base.SetProperty(property, ref reader, options);

            switch (property)
            {
                case "background":
                    Background = (Background?)JsonSerializer.Deserialize(ref reader, typeof(Background), options);
                    break;


                case "padding":
                    Padding = (Padding?)JsonSerializer.Deserialize(ref reader, typeof(Padding), options);
                    break;

                case "textBoxData":
                    TextBoxData = (List<TextBoxData>?)JsonSerializer.Deserialize(ref reader, typeof(List<TextBoxData>), options);
                    break;
            }
        }



        public async override Task SetData(IRepository repository)
        {
            if (Background != null && Background.Image != null)
            {
                await Background.Image.SetData(repository);
            }
        }
    }
}