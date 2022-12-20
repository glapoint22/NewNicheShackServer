using MediatR;
using Shared.Common.Classes;
using Shared.Common.Interfaces;

namespace Manager.Domain.Entities
{
    public sealed class Page : IPage
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? UrlName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int PageType { get; set; }


        private readonly List<PageSubniche> _pageSubniches = new();
        public IReadOnlyList<PageSubniche> PageSubniches => _pageSubniches.AsReadOnly();


        private readonly List<PageKeywordGroup> _pageKeywordGroups = new();
        public IReadOnlyList<PageKeywordGroup> PageKeywordGroups => _pageKeywordGroups.AsReadOnly();



        private readonly List<PageKeyword> _pageKeywords = new();
        public IReadOnlyList<PageKeyword> PageKeywords => _pageKeywords.AsReadOnly();



        public static Page Create(string name, string content, int pageType)
        {
            Page page = new()
            {
                Name = name,
                UrlName = Utility.GenerateUrlName(name),
                Content = content,
                PageType = pageType
            };

            return page;
        }




        public void AddPageSubniche(Guid subnicheId)
        {
            PageSubniche pageSubniche = PageSubniche.Create(Id, subnicheId);
            _pageSubniches.Add(pageSubniche);
        }



        public Page Duplicate()
        {
            Page page = new()
            {
                Name = Name + " Copy",
                Content = Content,
                UrlName = UrlName,
                PageType = PageType
            };

            if (_pageSubniches.Count > 0)
            {
                foreach (PageSubniche pageSubniche in _pageSubniches)
                {
                    page.AddPageSubniche(pageSubniche.SubnicheId);
                }
            }


            if (_pageKeywordGroups.Count > 0)
            {
                foreach (PageKeywordGroup pageKeywordGroup in _pageKeywordGroups)
                {
                    page.AddPageKeywordGroup(pageKeywordGroup.KeywordGroup);
                }
            }


            return page;
        }


        public void Update(string name, string content, int pageType)
        {
            Name = name;
            Content = content;
            PageType = pageType;

            if (PageType == (int)Shared.PageBuilder.Enums.PageType.Custom || PageType == (int)Shared.PageBuilder.Enums.PageType.Browse)
            {
                UrlName = Utility.GenerateUrlName(name);
            }
            else
            {
                UrlName = null;
            }

            // Remove any page subniches or page keyword groups
            _pageSubniches.Clear();
            _pageKeywordGroups.Clear();
        }



        public void AddPageKeywordGroup(KeywordGroup keywordGroup)
        {
            // Add the page keyword group
            PageKeywordGroup pageKeywordGroup = PageKeywordGroup.Create(Id, keywordGroup.Id);
            _pageKeywordGroups.Add(pageKeywordGroup);

            // Add the page keywords
            foreach (KeywordInKeywordGroup keywordInKeywordGroup in keywordGroup.KeywordsInKeywordGroup)
            {
                AddPageKeyword(keywordInKeywordGroup.Id);
            }
        }




        public void AddPageKeyword(Guid keywordInKeywordGroupId)
        {
            PageKeyword pageKeyword = PageKeyword.Create(Id, keywordInKeywordGroupId);
            _pageKeywords.Add(pageKeyword);
        }



        public void RemovePageKeyword(Guid keywordInKeywordGroupId)
        {
            PageKeyword pageKeyword = _pageKeywords
                .Where(x => x.KeywordInKeywordGroupId == keywordInKeywordGroupId)
                .Single();

            _pageKeywords.Remove(pageKeyword);
        }


        public void RemovePageKeywordGroup()
        {
            _pageKeywordGroups.Clear();
            _pageKeywords.Clear();
        }
    }
}