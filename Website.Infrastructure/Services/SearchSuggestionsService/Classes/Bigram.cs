using Website.Infrastructure.Services.SearchSuggestionsService.Interfaces;

namespace Website.Infrastructure.Services.SearchSuggestionsService.Classes
{
    public sealed class Bigram : INgram
    {
        public Tuple<string, string> Value { get; set; }


        public Bigram(string word1, string word2)
        {
            Value = new Tuple<string, string>(word1, word2);
        }

        public List<string> ToList()
        {
            return new List<string>() { Value.Item1, Value.Item2 };
        }


        public string ToSearchTerm()
        {
            return Value.Item1 + " " + Value.Item2;
        }
    }
}