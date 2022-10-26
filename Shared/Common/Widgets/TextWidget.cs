using Shared.Common.Classes;
using Shared.Common.Interfaces;

namespace Shared.Common.Widgets
{
    public sealed class TextWidget : Widget
    {
        private readonly IRepository _repository;

        public TextWidget(IRepository repository)
        {
            _repository = repository;
        }

        public Background Background { get; set; } = null!;
        public Padding Padding { get; set; } = null!;
        public List<TextBoxData> TextBoxData { get; set; } = null!;
    }
}