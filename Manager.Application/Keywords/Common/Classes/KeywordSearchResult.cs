using Manager.Application.Common.Classes;

namespace Manager.Application.Keywords.Common.Classes
{
    public sealed record KeywordSearchResult : SearchResult
    {
        public bool ForProduct { get; set; }
        public bool Checked { get; set; }
    }
}