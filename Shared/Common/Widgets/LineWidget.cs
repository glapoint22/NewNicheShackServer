using Shared.Common.Classes;

namespace Shared.Common.Widgets
{
    public sealed class LineWidget : Widget
    {
        public Border Border { get; set; } = null!;
        public Shadow Shadow { get; set; } = null!;
    }
}