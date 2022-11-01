namespace Website.Infrastructure.Services.SearchSuggestionsService.Classes
{
    public sealed class BigramData : NgramData
    {
        private readonly Dictionary<string, List<string>> _wordList;
        private readonly Dictionary<string, List<KeyValuePair<int, int>>> _wordIndices;
        private readonly Dictionary<string, Dictionary<string, string>> _partialWords;

        public BigramData(Dictionary<string, List<string>> wordList, Dictionary<string, List<KeyValuePair<int, int>>> wordIndices, Dictionary<string, Dictionary<string, string>> partialWords)
        {
            _wordList = wordList;
            _wordIndices = wordIndices;
            _partialWords = partialWords;
        }



        public NgramList<Bigram>? GetBigrams(string word1, string word2)
        {
            if (!_wordList.ContainsKey(word1)) return null;

            List<Bigram> bigrams = GetWords(word2, _wordList[word1], _wordIndices[word1])
                .Select(x => new Bigram(word1, x))
            .ToList();
            if (bigrams.Count == 0) return null;

            return new NgramList<Bigram>(bigrams, new Bigram(word1, word2));
        }


        public Bigram? GetBigram(Unigram unigram, string partialWord)
        {
            if (!_partialWords.ContainsKey(unigram.Value) || !_partialWords[unigram.Value].ContainsKey(partialWord)) return null;
            string fullWord = _partialWords[unigram.Value][partialWord];
            return new Bigram(unigram.Value, fullWord);
        }




        public NgramList<Bigram>? GetBigrams(NgramList<Unigram> unigrams, string referenceWord)
        {
            List<Bigram> bigrams = new List<Bigram>();

            foreach (Unigram unigram in unigrams.Ngrams)
            {
                NgramList<Bigram>? bigramList = GetBigrams(unigram.Value, referenceWord);

                if (bigramList != null)
                {
                    bigrams.AddRange(bigramList.Ngrams);
                }
            }
            if (bigrams.Count == 0) return null;

            return new NgramList<Bigram>(bigrams, new Bigram(unigrams.Reference.Value, referenceWord));
        }
    }
}