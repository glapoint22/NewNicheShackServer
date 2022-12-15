using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.DeleteAvailableKeywordGroup.Commands
{
    public sealed class DeleteAvailableKeywordGroupCommandHandler : IRequestHandler<DeleteAvailableKeywordGroupCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DeleteAvailableKeywordGroupCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteAvailableKeywordGroupCommand request, CancellationToken cancellationToken)
        {
            KeywordGroup keywordGroup = await _dbContext.KeywordGroups
                .Where(x => x.Id == request.KeywordGroupId)
                .Include(x => x.KeywordsInKeywordGroup)
                .SingleAsync();

            List<Keyword> keywords = await _dbContext.Keywords
                .Where(x => keywordGroup.KeywordsInKeywordGroup
                    .Select(z => z.KeywordId)
                    .Contains(x.Id) && x.KeywordsInKeywordGroup.Count == 1)
                .ToListAsync();

            List<ProductKeyword> productKeywords = await _dbContext.ProductKeywords
                .Where(x => keywordGroup.KeywordsInKeywordGroup
                    .Select(z => z.KeywordId)
                    .Contains(x.KeywordId) && 
                        x.Keyword.KeywordsInKeywordGroup.Count > 1 && 
                        x.Product.KeywordGroupsBelongingToProduct.Count == 1)
                .ToListAsync();

            _dbContext.Keywords.RemoveRange(keywords);
            _dbContext.ProductKeywords.RemoveRange(productKeywords);
            _dbContext.KeywordGroups.Remove(keywordGroup);

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}