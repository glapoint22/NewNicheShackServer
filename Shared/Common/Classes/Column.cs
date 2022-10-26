using Shared.Common.Widgets;
using Shared.PageBuilder.Classes;

namespace Shared.Common.Classes
{
    public sealed class Column
    {
        public Background Background { get; set; } = null!;
        public Border Border { get; set; } = null!;
        public Corners Corners { get; set; } = null!;
        public Shadow Shadow { get; set; } = null!;
        public Padding Padding { get; set; } = null!;
        public float Width { get; set; }
        public ColumnSpan ColumnSpan { get; set; } = null!;
        public HorizontalAlignment HorizontalAlignment { get; set; } = null!;
        public Widget WidgetData { get; set; } = null!;
    }
}