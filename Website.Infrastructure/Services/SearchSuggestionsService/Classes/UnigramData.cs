namespace Website.Infrastructure.Services.SearchSuggestionsService.Classes
{
    public sealed class UnigramData : NgramData
    {
        private readonly List<string> _wordList;
        private readonly List<KeyValuePair<int, int>> _wordIndices;

        public UnigramData(List<string> wordList, List<KeyValuePair<int, int>> wordIndices)
        {
            _wordList = wordList;
            _wordIndices = wordIndices;
        }

        public NgramList<Unigram>? GetUnigrams(string word)
        {
            List<Unigram> unigrams = GetWords(word, _wordList, _wordIndices)
                .Select(x => new Unigram(x))
                .ToList();

            if (unigrams.Count == 0) return null;

            return new NgramList<Unigram>(unigrams, new Unigram(word));
        }
    }
}