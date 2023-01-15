using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Keywords.UpdateKeywordName.Commands
{
    public sealed class UpdateKeywordNameCommandHandler : IRequestHandler<UpdateKeywordNameCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public UpdateKeywordNameCommandHandler(IManagerDbContext dbContext, IWebsiteDbContext websiteDbContext)
        {
            _dbContext = dbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(UpdateKeywordNameCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Keyword? keyword = await _dbContext.Keywords
                .Where(x => x.Name == request.Name.Trim().ToLower())
                .SingleOrDefaultAsync();

            if (keyword != null)
            {
                // Remove the old keyword
                Domain.Entities.Keyword oldKeyword = (await _dbContext.Keywords.FindAsync(request.Id))!;
                _dbContext.Keywords.Remove(oldKeyword);

                Website.Domain.Entities.Keyword? oldWebsiteKeyword = await _websiteDbContext.Keywords.FindAsync(request.Id);
                if (oldWebsiteKeyword != null)
                {
                    _websiteDbContext.Keywords.Remove(oldWebsiteKeyword);
                }

                // Get all the keyword groups the old keyword was in
                var keywordGroupIds = await _dbContext.KeywordsInKeywordGroup
                    .Where(x => x.KeywordId == oldKeyword.Id)
                    .Select(x => x.KeywordGroupId)
                    .ToListAsync();

                // Add the keyword to the groups
                _dbContext.KeywordsInKeywordGroup
                    .AddRange(keywordGroupIds
                        .Select(keywordGroupId => Domain.Entities.KeywordInKeywordGroup.Create(keyword, keywordGroupId)));


                // Get all the products the old keyword was part of
                var productIds = await _dbContext.ProductKeywords
                    .Where(x => x.KeywordId == oldKeyword.Id && x.Product.KeywordGroupsBelongingToProduct
                        .Count(z => z.KeywordGroup.KeywordsInKeywordGroup
                            .Any(q => q.KeywordId == keyword.Id)) == 0)
                    .Select(x => x.ProductId)
                    .ToListAsync();

                // Add the keyword to the product
                if (productIds.Count > 0)
                {
                    _dbContext.ProductKeywords
                        .AddRange(productIds.Select(x => Domain.Entities.ProductKeyword.Create(x, keyword)));

                    _websiteDbContext.ProductKeywords
                        .AddRange(productIds.Select(x => new Website.Domain.Entities.ProductKeyword
                        {
                            ProductId = x,
                            KeywordId = keyword.Id
                        }));
                }
            }
            else
            {
                keyword = (await _dbContext.Keywords.FindAsync(request.Id))!;
                keyword.UpdateName(request.Name);

                Website.Domain.Entities.Keyword? websiteKeyword = await _websiteDbContext.Keywords.FindAsync(request.Id);

                if (websiteKeyword != null)
                {
                    websiteKeyword.Name = keyword.Name;
                }
            }

            await _dbContext.SaveChangesAsync();
            await _websiteDbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}