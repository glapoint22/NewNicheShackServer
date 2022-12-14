using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.DeleteSelectedKeyword.Commands
{
    public sealed class DeleteSelectedKeywordCommandHandler : IRequestHandler<DeleteSelectedKeywordCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DeleteSelectedKeywordCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteSelectedKeywordCommand request, CancellationToken cancellationToken)
        {
            var count = await _dbContext.KeywordsInKeywordGroup
                .CountAsync(x => x.KeywordId == request.KeywordId);

            if (count == 1)
            {
                Keyword keyword = await _dbContext.Keywords
                    .Where(x => x.Id == request.KeywordId)
                    .SingleAsync();

                _dbContext.Keywords.Remove(keyword);
            }
            else
            {
                // Remove the keyword from the group
                KeywordInKeywordGroup keywordInKeywordGroup = await _dbContext.KeywordsInKeywordGroup
                    .Where(x => x.KeywordId == request.KeywordId && x.KeywordGroupId == request.KeywordGroupId)
                    .SingleAsync();

                _dbContext.KeywordsInKeywordGroup.Remove(keywordInKeywordGroup);

                // Remove the keyword from the product
                ProductKeyword? productKeyword = await _dbContext.ProductKeywords
                    .Where(x => x.KeywordId == request.KeywordId &&
                        x.ProductId == x.Product.KeywordGroupsBelongingToProduct
                            .Where(z => z.KeywordGroupId == request.KeywordGroupId)
                            .Select(z => z.ProductId)
                            .Single() &&

                        // No other keyword groups in this product contains this keyword
                        !x.Product.KeywordGroupsBelongingToProduct
                            .Any(z => z.KeywordGroup.KeywordsInKeywordGroup
                                .Where(x => x.KeywordGroupId != request.KeywordGroupId)
                                .Select(q => q.KeywordId)
                                .Contains(request.KeywordId)))
                    .SingleOrDefaultAsync();

                if (productKeyword != null)
                    _dbContext.ProductKeywords.Remove(productKeyword);
            }

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}