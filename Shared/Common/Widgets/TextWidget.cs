using Shared.Common.Classes;

namespace Shared.Common.Widgets
{
    public sealed class TextWidget : Widget
    {
        public Background Background { get; set; } = null!;
        public Padding Padding { get; set; } = null!;
        public List<TextBoxData> TextBoxData { get; set; } = null!;
    }
}