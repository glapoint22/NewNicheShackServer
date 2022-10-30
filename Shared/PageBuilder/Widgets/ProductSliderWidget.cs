using Shared.Common.Classes;
using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using Shared.PageBuilder.Classes;

namespace Shared.PageBuilder.Widgets
{
    public sealed class ProductSliderWidget : Widget
    {
        private readonly IRepository _repository;

        public ProductSliderWidget(IRepository repository)
        {
            _repository = repository;
        }

        public Caption Caption { get; set; } = null!;
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
        //public Query Query { get; set; }
    }
}