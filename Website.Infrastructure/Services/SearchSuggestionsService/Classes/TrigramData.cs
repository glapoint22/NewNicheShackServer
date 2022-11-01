namespace Website.Infrastructure.Services.SearchSuggestionsService.Classes
{
    public sealed class TrigramData : NgramData
    {
        private readonly Dictionary<Tuple<string, string>, List<string>> _wordList;
        private readonly Dictionary<Tuple<string, string>, List<KeyValuePair<int, int>>> _wordIndices;
        private readonly Dictionary<Tuple<string, string>, Dictionary<string, string>> _partialWords;

        public TrigramData(Dictionary<Tuple<string, string>, List<string>> wordList, Dictionary<Tuple<string, string>, List<KeyValuePair<int, int>>> wordIndices, Dictionary<Tuple<string, string>, Dictionary<string, string>> partialWords)
        {
            _wordList = wordList;
            _wordIndices = wordIndices;
            _partialWords = partialWords;
        }




        public NgramList<Trigram>? GetTrigrams(string word1, string word2, string word3)
        {
            if (!_wordList.ContainsKey(new Tuple<string, string>(word1, word2))) return null;

            List<Trigram> trigrams = GetWords(word3, _wordList[new Tuple<string, string>(word1, word2)], _wordIndices[new Tuple<string, string>(word1, word2)])
                .Select(x => new Trigram(word1, word2, x))
                .ToList();

            if (trigrams.Count == 0) return null;

            return new NgramList<Trigram>(trigrams, new Trigram(word1, word2, word3));
        }




        public Trigram? GetTrigram(Bigram bigram, string partialWord)
        {
            if (!_partialWords.ContainsKey(bigram.Value) || !_partialWords[bigram.Value].ContainsKey(partialWord)) return null;
            string fullWord = _partialWords[bigram.Value][partialWord];

            return new Trigram(bigram.Value.Item1, bigram.Value.Item2, fullWord);
        }




        public NgramList<Trigram>? GetTrigrams(NgramList<Bigram> bigrams, string referenceWord)
        {
            List<Trigram> trigrams = new List<Trigram>();

            foreach (Bigram bigram in bigrams.Ngrams)
            {
                NgramList<Trigram>? trigramList = GetTrigrams(bigram.Value.Item1, bigram.Value.Item2, referenceWord);

                if (trigramList != null)
                {
                    trigrams.AddRange(trigramList.Ngrams);
                }
            }

            if (trigrams.Count == 0) return null;

            return new NgramList<Trigram>(trigrams, new Trigram(bigrams.Reference.Value.Item1, bigrams.Reference.Value.Item2, referenceWord));
        }
    }
}