namespace Shared.Common.Classes
{
    public sealed class Row
    {
        public float Top { get; set; }
        public Background Background { get; set; } = null!;
        public Border Border { get; set; } = null!;
        public Corners Corners { get; set; } = null!;
        public Shadow Shadow { get; set; } = null!;
        public Padding Padding { get; set; } = null!;
        public VerticalAlignment VerticalAlignment { get; set; } = null!;
        public List<Column> Columns { get; set; } = null!;
        public float RelativeTop { get; set; }
    }
}