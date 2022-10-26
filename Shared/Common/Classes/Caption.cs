namespace Shared.Common.Classes
{
    public sealed class Caption
    {
        public string Text { get; set; } = string.Empty;
        public string FontWeight { get; set; } = string.Empty;
        public string FontStyle { get; set; } = string.Empty;
        public string TextDecoration { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public KeyValuePair<string, string> Font { get; set; }
        public KeyValuePair<string, string> FontSize { get; set; }
    }
}