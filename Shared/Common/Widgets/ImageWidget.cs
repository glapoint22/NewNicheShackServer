using Shared.Common.Classes;

namespace Shared.Common.Widgets
{
    public sealed class ImageWidget : Widget
    {
        public PageImage Image { get; set; } = null!;
        public Border Border { get; set; } = null!;
        public Corners Corners { get; set; } = null!;
        public Shadow Shadow { get; set; } = null!;
        public Link Link { get; set; } = null!;
    }
}