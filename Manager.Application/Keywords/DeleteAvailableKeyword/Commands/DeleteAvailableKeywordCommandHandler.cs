using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.DeleteAvailableKeyword.Commands
{
    public sealed class DeleteAvailableKeywordCommandHandler : IRequestHandler<DeleteAvailableKeywordCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DeleteAvailableKeywordCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteAvailableKeywordCommand request, CancellationToken cancellationToken)
        {
            int count = await _dbContext.KeywordsInKeywordGroup
                .CountAsync(x => x.KeywordId == request.KeywordId);


            KeywordInKeywordGroup keywordInKeywordGroup = await _dbContext.KeywordsInKeywordGroup
                .Where(x => x.KeywordId == request.KeywordId && x.KeywordGroupId == request.KeywordGroupId)
                .Include(x => x.Keyword)
                .SingleAsync();

            _dbContext.KeywordsInKeywordGroup.Remove(keywordInKeywordGroup);

            if (count == 1)
            {
                _dbContext.Keywords.Remove(keywordInKeywordGroup.Keyword);
            }
            else
            {
                List<ProductKeyword> productKeywords = await _dbContext.ProductKeywords
                    .Where(x => x.KeywordId == request.KeywordId && 
                        x.Product.KeywordGroupsBelongingToProduct.Count == 1)
                    .ToListAsync();

                _dbContext.ProductKeywords.RemoveRange(productKeywords);
            }

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}