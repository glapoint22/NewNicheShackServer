namespace Shared.PageBuilder.Widgets.GridWidget.Classes
{
    public sealed class FilterParam
    {
        public string Name { get; set; } = string.Empty;
        public List<int> Values { get; set; } = new List<int>();
    }
}