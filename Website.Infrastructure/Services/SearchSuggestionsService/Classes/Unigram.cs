using Website.Infrastructure.Services.SearchSuggestionsService.Interfaces;

namespace Website.Infrastructure.Services.SearchSuggestionsService.Classes
{
    public sealed class Unigram : INgram
    {
        public string Value { get; set; }

        public Unigram(string word)
        {
            Value = word;
        }

        public List<string> ToList()
        {
            return new List<string>() { Value };
        }

        public string ToSearchTerm()
        {
            return Value;
        }
    }
}