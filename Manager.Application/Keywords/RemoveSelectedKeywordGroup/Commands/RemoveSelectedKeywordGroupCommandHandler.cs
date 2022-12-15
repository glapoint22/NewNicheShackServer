using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.RemoveSelectedKeywordGroup.Commands
{
    public sealed class RemoveSelectedKeywordGroupCommandHandler : IRequestHandler<RemoveSelectedKeywordGroupCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public RemoveSelectedKeywordGroupCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
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
            _dbContext.ProductKeywords.RemoveRange(productKeywords);
            _dbContext.Keywords.RemoveRange(keywords);

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