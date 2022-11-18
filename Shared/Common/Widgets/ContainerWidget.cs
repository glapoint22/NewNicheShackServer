using Shared.Common.Classes;

namespace Shared.Common.Widgets
{
    public sealed class ContainerWidget : Widget
    {
        public Background Background { get; set; } = null!;
        public Border Border { get; set; } = null!;
        public Corners Corners { get; set; } = null!;
        public Shadow Shadow { get; set; } = null!;
        public List<Row> Rows { get; set; } = new List<Row>();
    }
}