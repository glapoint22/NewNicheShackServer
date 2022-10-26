using Shared.Common.Classes;

namespace Shared.PageBuilder.Classes
{
    public sealed class WebPage
    {
        public Background Background { get; set; } = null!;
        public List<Row> Rows { get; set; } = new List<Row>();
    }
}