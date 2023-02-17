using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manager.Application.Common.Classes
{
    public class HierarchyItemCreated
    {
        protected readonly IManagerDbContext _dbContext;

        public HierarchyItemCreated(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // --------------------------------------------------------------------------- Create Keyword Group -----------------------------------------------------------------------
        protected async Task<KeywordGroup> CreateKeywordGroup(string name, bool forProduct = false)
        {
            KeywordGroup? keywordGroup = await _dbContext.KeywordGroups
                .Where(x => x.Name.Trim().ToLower() == name.Trim().ToLower())
                .SingleOrDefaultAsync();

            if (keywordGroup == null)
            {
                keywordGroup = KeywordGroup.Create(name, forProduct);
                _dbContext.KeywordGroups.Add(keywordGroup);
            }

            return keywordGroup;
        }












        // ------------------------------------------------------------------------------ Create Keyword --------------------------------------------------------------------------
        protected async Task<Keyword> CreateKeyword(string name)
        {
            Keyword? keyword = await _dbContext.Keywords
                .Where(x => x.Name.Trim().ToLower() == name.Trim().ToLower())
                .SingleOrDefaultAsync();

            if (keyword == null)
            {
                keyword = Keyword.Create(name);
                _dbContext.Keywords.Add(keyword);
            }

            return keyword;
        }








        // --------------------------------------------------------------------------- Add Keyword To Group -----------------------------------------------------------------------
        protected async Task AddKeywordToGroup(Keyword keyword, KeywordGroup keywordGroup)
        {
            KeywordInKeywordGroup? keywordInKeywordGroup = await _dbContext.KeywordsInKeywordGroup
                .Where(x => x.KeywordGroupId == keywordGroup.Id && x.KeywordId == keyword.Id)
                .SingleOrDefaultAsync();


            if (keywordInKeywordGroup == null)
            {
                keywordInKeywordGroup = KeywordInKeywordGroup.Create(keyword, keywordGroup.Id);
                _dbContext.KeywordsInKeywordGroup.Add(keywordInKeywordGroup);
            }
        }
    }
}