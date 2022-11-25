using Shared.Common.Dtos;

namespace Website.Application.Common.Classes
{
    public sealed class KeywordData
    {
        public string Name { get; set; } = string.Empty;
        public int SearchVolume { get; set; }
        public NicheDto Niche { get; set; } = null!;
    }
}