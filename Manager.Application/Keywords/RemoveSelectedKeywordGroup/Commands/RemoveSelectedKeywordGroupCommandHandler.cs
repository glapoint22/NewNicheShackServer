using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Keywords.RemoveSelectedKeywordGroup.Commands
{
    public sealed class RemoveSelectedKeywordGroupCommandHandler : IRequestHandler<RemoveSelectedKeywordGroupCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly Application.Common.Interfaces.IAuthService _authService;
        private readonly IWebsiteDbContext _websiteDbContext;

        public RemoveSelectedKeywordGroupCommandHandler(IManagerDbContext dbContext, Application.Common.Interfaces.IAuthService authService, IWebsiteDbContext websiteDbContext)
        {
            _dbContext = dbContext;
            _authService = authService;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(RemoveSelectedKeywordGroupCommand request, CancellationToken cancellationToken)
        {
            KeywordGroupBelongingToProduct keywordGroupBelongingToProduct = await _dbContext.KeywordGroupsBelongingToProduct
                .Where(x => x.ProductId == request.ProductId && x.KeywordGroupId == request.KeywordGroupId)
                .Include(x => x.KeywordGroup.KeywordsInKeywordGroup)
                .SingleAsync();

            List<ProductKeyword> productKeywords = await _dbContext.ProductKeywords
                .Where(x => keywordGroupBelongingToProduct.KeywordGroup.KeywordsInKeywordGroup
                    .Select(z => z.KeywordId)
                    .Contains(x.KeywordId) &&
                        x.ProductId == request.ProductId &&

                        // No other keyword groups in this product contains the keywords that are in the selected keyword group
                        !x.Product.KeywordGroupsBelongingToProduct
                            .Any(z => z.KeywordGroup.KeywordsInKeywordGroup
                                .Where(x => x.KeywordGroupId != request.KeywordGroupId)
                                .Select(q => q.KeywordId)
                                .Contains(x.KeywordId)))
                .ToListAsync();


            // Get keywords to delete but only if they are part of a custom keyword group
            List<Keyword> keywords = await _dbContext.Keywords
                .Where(x => keywordGroupBelongingToProduct.KeywordGroup.KeywordsInKeywordGroup
                    .Select(z => z.KeywordId)
                    .Contains(x.Id) &&
                        x.KeywordsInKeywordGroup
                            .Any(z => z.KeywordGroupId == request.KeywordGroupId && z.KeywordGroup.ForProduct) &&
                        x.KeywordsInKeywordGroup.Count == 1)
                .ToListAsync();

            _dbContext.KeywordGroupsBelongingToProduct.Remove(keywordGroupBelongingToProduct);


            if (productKeywords.Count > 0 || keywords.Count > 0)
            {
                _dbContext.ProductKeywords.RemoveRange(productKeywords);
                _dbContext.Keywords.RemoveRange(keywords);

                string userId = _authService.GetUserIdFromClaims();
                productKeywords[0].AddDomainEvent(new ProductModifiedEvent(request.ProductId, userId));





                List<Website.Domain.Entities.Keyword> websiteKeywords = await _websiteDbContext.Keywords
                .Where(x => keywords
                    .Select(x => x.Id)
                    .Contains(x.Id))
                .ToListAsync();

                _websiteDbContext.Keywords.RemoveRange(websiteKeywords);



                List<Website.Domain.Entities.ProductKeyword> websiteProductKeywords = await _websiteDbContext.ProductKeywords
                        .Where(x => productKeywords
                            .Select(z => z.KeywordId)
                            .Contains(x.KeywordId) &&
                                productKeywords
                                    .Select(z => z.ProductId)
                                    .Contains(x.ProductId))
                        .ToListAsync();

                _websiteDbContext.ProductKeywords.RemoveRange(websiteProductKeywords);

                await _websiteDbContext.SaveChangesAsync();
            }


            // Only delete if it is a custom keyword group
            if (keywordGroupBelongingToProduct.KeywordGroup.ForProduct)
            {
                _dbContext.KeywordGroups.Remove(keywordGroupBelongingToProduct.KeywordGroup);
            }

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}