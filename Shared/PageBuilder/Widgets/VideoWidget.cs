using Shared.Common.Classes;
using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using Shared.PageBuilder.Classes;

namespace Shared.PageBuilder.Widgets
{
    public sealed class VideoWidget : Widget
    {
        private readonly IRepository _repository;

        public VideoWidget(IRepository repository)
        {
            _repository = repository;
        }

        public Border Border { get; set; } = null!;
        public Corners Corners { get; set; } = null!;
        public Shadow Shadow { get; set; } = null!;
        public Video Video { get; set; } = null!;
    }
}