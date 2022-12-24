using Shared.Common.Classes;

namespace Shared.EmailBuilder.Classes
{
    public sealed class TableOptions
    {
        public float? Width { get; set; }
        public Background Background { get; set; } = null!;
        public string HorizontalAlignment { get; set; } = string.Empty;
        public bool CreateRow { get; set; }
    }
}