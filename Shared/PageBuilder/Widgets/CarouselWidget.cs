using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using Shared.PageBuilder.Classes;

namespace Shared.PageBuilder.Widgets
{
    public sealed class CarouselWidget : Widget
    {
        private readonly IRepository _repository;

        public CarouselWidget(IRepository repository)
        {
            _repository = repository;
        }

        public List<LinkableImage> Banners { get; set; } = new List<LinkableImage>();
    }
}