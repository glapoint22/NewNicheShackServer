using Shared.Common.Classes;
using Shared.Common.Dtos;
using Shared.Common.Widgets;

namespace Shared.PageBuilder.Widgets
{
    public sealed class ProductSliderWidget : Widget
    {
        public Caption Caption { get; set; } = null!;
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}