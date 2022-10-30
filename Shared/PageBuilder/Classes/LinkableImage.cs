using Shared.Common.Classes;

namespace Shared.PageBuilder.Classes
{
    public sealed class LinkableImage
    {
        public PageImage Image { get; set; } = null!;
        public Link Link { get; set; } = null!;
    }
}