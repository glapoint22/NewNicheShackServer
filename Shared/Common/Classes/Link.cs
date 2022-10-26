using Shared.Common.Enums;

namespace Shared.Common.Classes
{
    public sealed class Link
    {
        public int Id { get; set; }
        public LinkType LinkType { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}