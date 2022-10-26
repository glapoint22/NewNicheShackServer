using Shared.Common.Classes;
using Shared.Common.Interfaces;
using System.Text.Json;

namespace Shared.Common.Widgets
{
    public sealed class ButtonWidget : Widget
    {
        private readonly IRepository _repository;

        public ButtonWidget(IRepository repository)
        {
            _repository = repository;
        }

        public Background? Background { get; set; }
        public Border? Border { get; set; }
        public Caption? Caption { get; set; }
        public Corners? Corners { get; set; }
        public Shadow? Shadow { get; set; }
        public Padding? Padding { get; set; }
        public Link? Link { get; set; }
        public string? BackgroundHoverColor { get; set; }
        public string? BackgroundActiveColor { get; set; }
        public string? BorderHoverColor { get; set; }
        public string? BorderActiveColor { get; set; }
        public string? TextHoverColor { get; set; }
        public string? TextActiveColor { get; set; }



        public override void SetProperty(string? property, ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            base.SetProperty(property, ref reader, options);

            switch (property)
            {
                case "background":
                    Background = (Background?)JsonSerializer.Deserialize(ref reader, typeof(Background), options);
                    break;

                case "border":
                    Border = (Border?)JsonSerializer.Deserialize(ref reader, typeof(Border), options);
                    break;

                case "caption":
                    Caption = (Caption?)JsonSerializer.Deserialize(ref reader, typeof(Caption), options);
                    break;

                case "corners":
                    Corners = (Corners?)JsonSerializer.Deserialize(ref reader, typeof(Corners), options);
                    break;

                case "shadow":
                    Shadow = (Shadow?)JsonSerializer.Deserialize(ref reader, typeof(Shadow), options);
                    break;

                case "padding":
                    Padding = (Padding?)JsonSerializer.Deserialize(ref reader, typeof(Padding), options);
                    break;

                case "link":
                    Link = (Link?)JsonSerializer.Deserialize(ref reader, typeof(Link), options);
                    break;
                case "backgroundHoverColor":
                    BackgroundHoverColor = (string?)JsonSerializer.Deserialize(ref reader, typeof(string), options);
                    break;

                case "backgroundActiveColor":
                    BackgroundActiveColor = (string?)JsonSerializer.Deserialize(ref reader, typeof(string), options);
                    break;

                case "borderHoverColor":
                    BorderHoverColor = (string?)JsonSerializer.Deserialize(ref reader, typeof(string), options);
                    break;

                case "borderActiveColor":
                    BorderActiveColor = (string?)JsonSerializer.Deserialize(ref reader, typeof(string), options);
                    break;

                case "textHoverColor":
                    TextHoverColor = (string?)JsonSerializer.Deserialize(ref reader, typeof(string), options);
                    break;

                case "textActiveColor":
                    TextActiveColor = (string?)JsonSerializer.Deserialize(ref reader, typeof(string), options);
                    break;
            }
        }
    }
}