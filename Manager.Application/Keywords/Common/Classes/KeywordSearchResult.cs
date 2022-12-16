namespace Manager.Application.Keywords.Common.Classes
{
    public sealed record KeywordSearchResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool ForProduct { get; set; }
        public bool Checked { get; set; }
    }
}