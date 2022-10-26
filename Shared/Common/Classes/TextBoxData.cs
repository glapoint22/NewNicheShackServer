using Shared.Common.Enums;

namespace Shared.Common.Classes
{
    public sealed class TextBoxData
    {
        public ElementType ElementType { get; set; }
        public List<TextBoxData> Children { get; set; } = new List<TextBoxData>();
        public string Text { get; set; } = string.Empty;
        public List<StyleData> Styles { get; set; } = new List<StyleData>();
        public Link Link { get; set; } = null!;
        public int? Indent { get; set; }
    }
}