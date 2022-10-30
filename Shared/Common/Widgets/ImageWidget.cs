using Shared.Common.Classes;
using Shared.Common.Interfaces;

namespace Shared.Common.Widgets
{
    public sealed class ImageWidget : Widget
    {
        private readonly IRepository _repository;

        public ImageWidget(IRepository repository)
        {
            _repository = repository;
        }

        public PageImage Image { get; set; } = null!;
        public Border Border { get; set; } = null!;
        public Corners Corners { get; set; } = null!;
        public Shadow Shadow { get; set; } = null!;
        public Link Link { get; set; } = null!;
    }
}