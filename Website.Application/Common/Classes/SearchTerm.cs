namespace Website.Application.Common.Classes
{
    public sealed class SearchTerm
    {
        public string Name { get; set; } = string.Empty;
        public List<NicheDto> Niches { get; set; } = new List<NicheDto>();
        public float SearchVolume { get; set; }
    }
}