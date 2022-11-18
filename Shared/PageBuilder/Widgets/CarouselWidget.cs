using Shared.Common.Widgets;
using Shared.PageBuilder.Classes;

namespace Shared.PageBuilder.Widgets
{
    public sealed class CarouselWidget : Widget
    {
        public List<LinkableImage> Banners { get; set; } = new List<LinkableImage>();
    }
}