﻿namespace Website.Infrastructure.Services.SearchSuggestionsService.Classes
{
    public sealed class QuadgramData : NgramData
    {
        private readonly Dictionary<Tuple<string, string, string>, List<string>> _wordList;
        private readonly Dictionary<Tuple<string, string, string>, List<KeyValuePair<int, int>>> _wordIndices;
        private readonly Dictionary<Tuple<string, string, string>, Dictionary<string, string>> _partialWords;

        public QuadgramData(Dictionary<Tuple<string, string, string>, List<string>> wordList, Dictionary<Tuple<string, string, string>, List<KeyValuePair<int, int>>> wordIndices, Dictionary<Tuple<string, string, string>, Dictionary<string, string>> partialWords)
        {
            _wordList = wordList;
            _wordIndices = wordIndices;
            _partialWords = partialWords;
        }



        public NgramList<Quadgram>? GetQuadgrams(string word1, string word2, string word3, string word4)
        {
            if (!_wordList.ContainsKey(new Tuple<string, string, string>(word1, word2, word3))) return null;

            List<Quadgram> quadgrams = GetWords(word4, _wordList[new Tuple<string, string, string>(word1, word2, word3)], _wordIndices[new Tuple<string, string, string>(word1, word2, word3)])
                .Select(x => new Quadgram(word1, word2, word3, x))
                .ToList();

            if (quadgrams.Count == 0) return null;

            return new NgramList<Quadgram>(quadgrams, new Quadgram(word1, word2, word3, word4));
        }



        public Quadgram? GetQuadgram(Trigram trigram, string partialWord)
        {
            if (!_partialWords.ContainsKey(trigram.Value) || !_partialWords[trigram.Value].ContainsKey(partialWord)) return null;
            string fullWord = _partialWords[trigram.Value][partialWord];

            return new Quadgram(trigram.Value.Item1, trigram.Value.Item2, trigram.Value.Item3, fullWord);
        }




        public NgramList<Quadgram>? GetQuadgrams(NgramList<Trigram> trigrams, string referenceWord)
        {
            List<Quadgram> quadgrams = new List<Quadgram>();

            foreach (Trigram trigram in trigrams.Ngrams)
            {
                NgramList<Quadgram>? quadgramList = GetQuadgrams(trigram.Value.Item1, trigram.Value.Item2, trigram.Value.Item3, referenceWord);

                if (quadgramList != null)
                {
                    quadgrams.AddRange(quadgramList.Ngrams);
                }
            }

            if (quadgrams.Count == 0) return null;

            return new NgramList<Quadgram>(quadgrams, new Quadgram(trigrams.Reference.Value.Item1, trigrams.Reference.Value.Item2, trigrams.Reference.Value.Item3, referenceWord));
        }
    }
}