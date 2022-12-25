using Shared.Common.Dtos;
using System.Text.RegularExpressions;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Infrastructure.Services.SearchSuggestionsService.Classes;

namespace Website.Infrastructure.Services.SearchSuggestionsService
{
    public sealed class SearchSuggestionsService : ISearchSuggestionsService
    {
        private Node _rootNode = null!;
        private Ngrams _ngrams = null!;


        // ------------------------------------------------------------------ Get Search Suggestions ------------------------------------------------------------------
        public List<SearchSuggestion>? GetSearchSuggestions(string? searchTerm, Guid nicheId)
        {
            if (_rootNode == null || searchTerm == null) return null;


            Node node = _rootNode;
            Regex regex = new Regex(@"[\s]{2,}");

            // Remove unwanted spaces
            searchTerm = searchTerm.TrimStart();
            searchTerm = regex.Replace(searchTerm, " ");


            bool searchTermCorrected = false;

            for (int i = 0; i < searchTerm.Length; i++)
            {
                char c = searchTerm[i];


                if (node.Children.ContainsKey(c))
                {
                    node = node.Children[c];
                }

                // No nodes exists for the current character
                else
                {
                    if (searchTermCorrected) return null;
                    searchTerm = GetCorrectedSearchTerm(searchTerm);
                    searchTermCorrected = true;

                    if (searchTerm == null) return null;

                    node = _rootNode;
                    i = -1;
                }
            }


            if (!node.Suggestions.ContainsKey(nicheId)) return null;

            var suggestions = node.Suggestions[nicheId]
                .Select(x => new SearchSuggestion
                {
                    Name = x.Name,
                    Niche = x.Niche!
                })
                .ToList();

            if (nicheId == Guid.Empty && suggestions[0].Niche != null) suggestions.Insert(0, new SearchSuggestion { Name = suggestions[0].Name });

            return suggestions;
        }






        // ------------------------------------------------------------------ Set Search Suggestions ------------------------------------------------------------------
        public void SetSearchSuggestions(List<KeywordData> keywords, List<Guid> nicheIds)
        {
            List<SearchTerm> searchTerms = GetSearchTerms(keywords);
            List<SplitSearchTerm> splitSearchTerms = GenerateSplitSearchTerms(searchTerms);

            _ngrams = new Ngrams(splitSearchTerms);
            _rootNode = new(splitSearchTerms, nicheIds);
        }









        // --------------------------------------------------------------------- Get Search Terms ---------------------------------------------------------------------
        private static List<SearchTerm> GetSearchTerms(List<KeywordData> keywords)
        {
            return keywords.GroupBy(x => x.Name, (key, x) => new
            {
                name = key,
                searchVolume = x.Select(z => z.SearchVolume)
                    .FirstOrDefault(),
                niches = x.Select(z => new
                {
                    z.Niche.Id,
                    z.Niche.Name,
                    z.Niche.UrlName,
                })
                .GroupBy(x => x.Id, (key, a) => new
                {
                    Id = key,
                    Name = a.Select(w => w.Name).FirstOrDefault(),
                    UrlName = a.Select(w => w.UrlName).FirstOrDefault(),
                })
            })
            .Select(x => new SearchTerm
            {
                Name = x.name,
                SearchVolume = x.searchVolume,
                Niches = x.niches
                    .Select(z => new NicheDto
                    {
                        Id = z.Id,
                        Name = z.Name!,
                        UrlName = z.UrlName!
                    })
                    .ToList()
            })
            .ToList();
        }







        // ---------------------------------------------------------------- Generate Split Search Terms ---------------------------------------------------------------
        private static List<SplitSearchTerm> GenerateSplitSearchTerms(List<SearchTerm> searchTerms)
        {
            List<SplitSearchTerm> splitSearchTerms = new List<SplitSearchTerm>();

            // Split the words by space
            var splitWords = searchTerms
                .Select(x => new
                {
                    WordArray = x.Name.Split(' '),
                    x.Niches,
                    x.SearchVolume,
                    x.Name

                })
                .ToList();


            foreach (var word in splitWords)
            {
                for (int i = 0; i < word.WordArray.Length; i++)
                {
                    if (i > 3) break;
                    string phrase = string.Empty;
                    for (int j = i; j < word.WordArray.Length; j++)
                    {
                        phrase += word.WordArray[j] + " ";
                    }

                    phrase = phrase.Trim();



                    splitSearchTerms.Add(new SplitSearchTerm
                    {
                        SearchTerm = phrase,
                        Niches = i == 0 ? word.Niches : null,
                        SearchVolume = i == 0 ? word.SearchVolume : 0,
                        Parents = i > 0 ? new List<SearchTerm>
                        {
                            new SearchTerm {
                                Name = word.Name,
                                Niches = word.Niches,
                                SearchVolume = word.SearchVolume
                            }
                        } : null

                    });
                }
            }

            return splitSearchTerms
                .GroupBy(x => x.SearchTerm, (key, x) => new SplitSearchTerm
                {
                    SearchTerm = key,
                    Niches = x.Select(z => z.Niches).FirstOrDefault(),
                    SearchVolume = x.Select(z => z.SearchVolume).FirstOrDefault(),
                    Parents = x.Where(z => z.Parents != null)
                        .Select(z => z.Parents?.FirstOrDefault()).ToList()!
                })
                .OrderBy(x => x.SearchTerm)
                .ToList();
        }









        // ----------------------------------------------------------------- Get Corrected Search Term ----------------------------------------------------------------
        private string? GetCorrectedSearchTerm(string searchTerm)
        {
            string[] wordArray = searchTerm.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // One word
            if (wordArray.Length == 1)
            {
                // Lets try to replace this word
                Unigram? unigram = GetUnigram(wordArray[0], out var unigrams);
                if (unigram != null) return unigram.ToSearchTerm();

            }

            // Two words
            else if (wordArray.Length == 2)
            {
                // Lets first try replaing the second word
                Bigram? bigram = GetBigram(wordArray[0], wordArray[1], out var bigrams);
                if (bigram != null) return bigram.ToSearchTerm();


                // We were not able to replace the second word. Lets try getting a new first word
                NgramList<Unigram>? unigrams;
                Unigram? unigram = GetUnigram(wordArray[0], out unigrams);
                if (unigram == null) return null;


                // Using the NEW first word, see if we can replace the second word
                bigram = GetBigram(unigram, wordArray[1]);
                if (bigram != null) return bigram.ToSearchTerm();



                // We were unable to replace the second word. Lets try replacing both words
                bigram = GetBigram(unigrams!, wordArray[1], out var bigramsOut);
                if (bigram != null) return bigram.ToSearchTerm();
            }


            // Three words
            else if (wordArray.Length == 3)
            {
                // Lets first try replaing the third word
                Trigram? trigram = GetTrigram(wordArray[0], wordArray[1], wordArray[2], out var trigrams);
                if (trigram != null) return trigram.ToSearchTerm();


                // We were unable to replace the third word. Lets try to replace the second word
                NgramList<Bigram>? bigrams;
                Bigram? bigram = GetBigram(wordArray[0], wordArray[1], out bigrams);


                // If the bigram is null, that means we were unsuccessful in replacing the second word
                if (bigram == null)
                {
                    // Lets try to replace the first word
                    NgramList<Unigram>? unigrams = _ngrams.UnigramData.GetUnigrams(wordArray[0]);
                    if (unigrams == null) return null;
                    bigram = GetBigram(unigrams, wordArray[1], out bigrams);
                    if (bigram == null) return null;
                }


                // If we made it this far, we were able to get the first two words(bigram).
                // Lets use this bigram to get all three words(our trigram)
                trigram = GetTrigram(bigram, wordArray[2]);
                if (trigram != null) return trigram.ToSearchTerm();



                // If we made it this far, we were not able to replace the third word
                // Lets try to replace all three words
                trigram = GetTrigram(bigrams!, wordArray[2], out var trigramsOut);
                if (trigram != null) return trigram.ToSearchTerm();
            }

            // Four or more words
            else if (wordArray.Length >= 4)
            {
                // Lets first try replaing the fourth word
                Quadgram? quadgram = GetQuadgram(wordArray[0], wordArray[1], wordArray[2], wordArray[3], out var quadgrams);
                if (quadgram != null)
                {
                    if (wordArray.Length == 4) return quadgram.ToSearchTerm();

                    return GetSearchTerm(quadgram, wordArray);
                }

                NgramList<Trigram>? trigrams;

                // We were unable to replace the fourth word. Lets try replacing the thrid word
                Trigram? trigram = GetTrigram(wordArray[0], wordArray[1], wordArray[2], out trigrams);



                // If the trigram is null, that means we were not able to replace the third word
                if (trigram == null)
                {
                    // Lets try replacing the second word
                    NgramList<Bigram>? bigrams = _ngrams.BigramData.GetBigrams(wordArray[0], wordArray[1]);

                    // If we have no bigrams, that means we were not able to replace the second word
                    if (bigrams == null)
                    {
                        // Lets try replacing the first word
                        NgramList<Unigram>? unigrams = _ngrams.UnigramData.GetUnigrams(wordArray[0]);
                        if (unigrams == null) return null;

                        // Lets use our unigrams to get our bigrams
                        bigrams = _ngrams.BigramData.GetBigrams(unigrams, wordArray[1]);
                        if (bigrams == null) return null;
                    }

                    // Now lets use our bigrams to get our trigram
                    trigram = GetTrigram(bigrams, wordArray[2], out trigrams);
                    if (trigram == null) return null;
                }

                // Now lets use our trigram to get the fourth word
                quadgram = GetQuadgram(trigram, wordArray[3]);
                if (quadgram != null)
                {
                    if (wordArray.Length == 4) return quadgram.ToSearchTerm();

                    return GetSearchTerm(quadgram, wordArray);
                }



                // We were unable to replace the fourth word.
                // Lets try replacing all four words
                quadgram = GetQuadgram(trigrams!, wordArray[3], out var quadgramsOut);
                if (quadgram != null)
                {
                    if (wordArray.Length == 4) return quadgram.ToSearchTerm();

                    return GetSearchTerm(quadgram, wordArray);
                }
            }
            return null;
        }







        // ----------------------------------------------------------------------- Get Unigram ------------------------------------------------------------------------
        private Unigram? GetUnigram(string word, out NgramList<Unigram>? unigrams)
        {
            unigrams = _ngrams.UnigramData.GetUnigrams(word);
            if (unigrams == null) return null;

            return unigrams.GetNgram();
        }







        // ----------------------------------------------------------------------- Get Bigram -------------------------------------------------------------------------
        private Bigram? GetBigram(string word1, string word2, out NgramList<Bigram>? bigrams)
        {
            bigrams = _ngrams.BigramData.GetBigrams(word1, word2);
            if (bigrams == null) return null;
            return bigrams.GetNgram();
        }







        // ----------------------------------------------------------------------- Get Bigram -------------------------------------------------------------------------
        private Bigram? GetBigram(Unigram unigram, string partialWord)
        {
            Bigram? bigram = _ngrams.BigramData.GetBigram(unigram, partialWord);
            if (bigram == null) return null;
            return bigram;
        }







        // ----------------------------------------------------------------------- Get Bigram -------------------------------------------------------------------------
        private Bigram? GetBigram(NgramList<Unigram> unigrams, string word, out NgramList<Bigram>? bigrams)
        {
            bigrams = _ngrams.BigramData.GetBigrams(unigrams, word);
            if (bigrams == null) return null;
            return bigrams.GetNgram();
        }







        // ----------------------------------------------------------------------- Get Trigram ------------------------------------------------------------------------
        private Trigram? GetTrigram(string word1, string word2, string word3, out NgramList<Trigram>? trigrams)
        {
            trigrams = _ngrams.TrigramData.GetTrigrams(word1, word2, word3);
            if (trigrams == null) return null;
            return trigrams.GetNgram();
        }







        // ----------------------------------------------------------------------- Get Trigram ------------------------------------------------------------------------
        private Trigram? GetTrigram(Bigram bigram, string partialWord)
        {
            Trigram? trigram = _ngrams.TrigramData.GetTrigram(bigram, partialWord);
            if (trigram == null) return null;
            return trigram;
        }






        // ----------------------------------------------------------------------- Get Trigram ------------------------------------------------------------------------
        private Trigram? GetTrigram(NgramList<Bigram> bigrams, string word, out NgramList<Trigram>? trigrams)
        {
            trigrams = _ngrams.TrigramData.GetTrigrams(bigrams, word);
            if (trigrams == null) return null;
            return trigrams.GetNgram();
        }





        // ----------------------------------------------------------------------- Get Quadgram -----------------------------------------------------------------------
        private Quadgram? GetQuadgram(string word1, string word2, string word3, string word4, out NgramList<Quadgram>? quadgrams)
        {
            quadgrams = _ngrams.QuadgramData.GetQuadgrams(word1, word2, word3, word4);
            if (quadgrams == null) return null;
            return quadgrams.GetNgram();
        }





        // ----------------------------------------------------------------------- Get Quadgram -----------------------------------------------------------------------
        private Quadgram? GetQuadgram(Trigram trigram, string partialWord)
        {
            Quadgram? quadgram = _ngrams.QuadgramData.GetQuadgram(trigram, partialWord);
            if (quadgram == null) return null;
            return quadgram;
        }






        // ----------------------------------------------------------------------- Get Quadgram -----------------------------------------------------------------------
        private Quadgram? GetQuadgram(NgramList<Trigram> trigrams, string word, out NgramList<Quadgram>? quadgrams)
        {
            quadgrams = _ngrams.QuadgramData.GetQuadgrams(trigrams, word);
            if (quadgrams == null) return null;
            return quadgrams.GetNgram();
        }





        // ---------------------------------------------------------------------- Get SearchTerm ----------------------------------------------------------------------
        private static string GetSearchTerm(Quadgram quadgram, string[] wordArray)
        {
            List<string> wordList = wordArray.Where((x, i) => i > 3).ToList();
            List<string> quadgramList = quadgram.ToList();
            quadgramList.AddRange(wordList);

            string searchTerm = string.Empty;
            quadgramList.ForEach(x => searchTerm += x + " ");

            return searchTerm.TrimEnd();
        }
    }
}