namespace Website.Infrastructure.Services.SearchSuggestionsService.Classes
{
    public sealed class Node
    {
        public char Char { get; }
        public Dictionary<char, Node> Children = new Dictionary<char, Node>();
        public Dictionary<Guid, List<SearchWordSuggestion>> Suggestions = new Dictionary<Guid, List<SearchWordSuggestion>>();

        public Node(char c)
        {
            Char = c;
        }


        public Node(List<SplitSearchTerm> splitSearchTerms, List<Guid> nicheIds)
        {
            Node rootNode = this;

            for (int i = 0; i < splitSearchTerms.Count; i++)
            {
                Node node = rootNode;

                for (int j = 0; j < splitSearchTerms[i].SearchTerm.Length; j++)
                {
                    // This is the current character in the phrase
                    char c = splitSearchTerms[i].SearchTerm[j];


                    // If this node existss
                    if (!node.Children.ContainsKey(c))
                    {
                        node.Children.Add(c, new Node(c));
                    }
                    else
                    {
                        node = node.Children[c];
                        continue;
                    }

                    node = node.Children[c];
                    string substring = splitSearchTerms[i].SearchTerm.Substring(0, j + 1);




                    // This is where we are adding suggestions based on the substring of the current phrase
                    // ie. ca would get cat, cart, car...
                    for (int k = i; k < splitSearchTerms.Count; k++)
                    {
                        SplitSearchTerm phrase = splitSearchTerms[k];
                        int len = Math.Min(j + 1, phrase.SearchTerm.Length);

                        if (phrase.SearchTerm.Substring(0, len) == substring)
                        {

                            // List of phrases we are using for suggestions
                            List<SplitSearchTerm> phraseList = new List<SplitSearchTerm>();
                            if (phrase.Parents?.Count > 0)
                            {
                                phraseList.AddRange(phrase.Parents.Select(x => new SplitSearchTerm
                                {
                                    SearchTerm = x.Name,
                                    Niches = x.Niches,
                                    SearchVolume = x.SearchVolume
                                }));
                            }
                            else
                            {
                                phraseList.Add(phrase);
                            }


                            // Add the phrases to the suggestions list based on niche id
                            foreach (SplitSearchTerm currentPhrase in phraseList)
                            {
                                for (int l = 0; l < nicheIds.Count; l++)
                                {
                                    Guid nicheId = nicheIds[l];


                                    if (l == 0 || currentPhrase.Niches!.Select(x => x.Id).Contains(nicheId))
                                    {
                                        if (!node.Suggestions.ContainsKey(nicheId)) node.Suggestions.Add(nicheId, new List<SearchWordSuggestion>());
                                    }
                                    else
                                    {
                                        continue;
                                    }

                                    var suggestions = node.Suggestions[nicheId];




                                    if (!suggestions.Select(x => x.Name).Contains(currentPhrase.SearchTerm))
                                    {
                                        suggestions.Add(new SearchWordSuggestion
                                        {
                                            Name = currentPhrase.SearchTerm,
                                            Niche = l == 0 && currentPhrase.Niches != null && currentPhrase.Niches.Count > 1 ? currentPhrase.Niches.FirstOrDefault() : null,
                                            SearchVolume = currentPhrase.SearchVolume
                                        });
                                    }
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }



                    // This will order the suggestions by search volume and limit the number of suggestions to 10
                    node.Suggestions = node.Suggestions
                        .ToDictionary(x => x.Key, x => x.Value
                            .OrderByDescending(z => z.SearchVolume)
                            .Select((z, i) => new SearchWordSuggestion
                            {
                                Name = z.Name,
                                Niche = (i == 0 ? z.Niche : null),
                                SearchVolume = z.SearchVolume
                            })
                            .Take(10)
                            .ToList());
                }
            }
        }
    }
}