using Shared.Common.Classes;
using Shared.Common.Interfaces;

namespace Shared.Common.Widgets
{
    public sealed class ContainerWidget : Widget
    {
        private readonly IRepository _repository;

        public ContainerWidget(IRepository repository)
        {
            _repository = repository;
        }

        public Background Background { get; set; } = null!;
        public Border Border { get; set; } = null!;
        public Corners Corners { get; set; } = null!;
        public Shadow Shadow { get; set; } = null!;
        public List<Row> Rows { get; set; } = new List<Row>();
    }
}