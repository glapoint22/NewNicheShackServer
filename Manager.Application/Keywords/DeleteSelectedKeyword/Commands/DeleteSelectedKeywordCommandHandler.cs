using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Keywords.DeleteSelectedKeyword.Commands
{
    public sealed class DeleteSelectedKeywordCommandHandler : IRequestHandler<DeleteSelectedKeywordCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly Application.Common.Interfaces.IAuthService _authService;
        private readonly IWebsiteDbContext _websiteDbContext;

        public DeleteSelectedKeywordCommandHandler(IManagerDbContext dbContext, Application.Common.Interfaces.IAuthService authService, IWebsiteDbContext websiteDbContext)
        {
            _dbContext = dbContext;
            _authService = authService;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(DeleteSelectedKeywordCommand request, CancellationToken cancellationToken)
        {
            string userId = _authService.GetUserIdFromClaims();
            var count = await _dbContext.KeywordsInKeywordGroup
                .CountAsync(x => x.KeywordId == request.KeywordId);

            if (count == 1)
            {
                Keyword keyword = await _dbContext.Keywords
                    .Where(x => x.Id == request.KeywordId)
                    .SingleAsync();

                _dbContext.Keywords.Remove(keyword);
                keyword.AddDomainEvent(new ProductModifiedEvent(request.ProductId, userId));


                Website.Domain.Entities.Keyword? websiteKeyword = await _websiteDbContext.Keywords.FindAsync(request.KeywordId);
                if (websiteKeyword != null)
                {
                    _websiteDbContext.Keywords.Remove(websiteKeyword);
                    await _websiteDbContext.SaveChangesAsync();
                }
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
                        x.ProductId == request.ProductId &&

                        // No other keyword groups in this product contains this keyword
                        !x.Product.KeywordGroupsBelongingToProduct
                            .Any(z => z.KeywordGroup.KeywordsInKeywordGroup
                                .Where(x => x.KeywordGroupId != request.KeywordGroupId)
                                .Select(q => q.KeywordId)
                                .Contains(request.KeywordId)))
                    .SingleOrDefaultAsync();

                if (productKeyword != null)
                {
                    _dbContext.ProductKeywords.Remove(productKeyword);
                    productKeyword.AddDomainEvent(new ProductModifiedEvent(request.ProductId, userId));

                    Website.Domain.Entities.ProductKeyword? websiteProductKeyword = await _websiteDbContext.ProductKeywords
                        .Where(x => x.KeywordId == productKeyword.KeywordId && x.ProductId == productKeyword.ProductId)
                        .SingleOrDefaultAsync();

                    if (websiteProductKeyword != null)
                    {
                        _websiteDbContext.ProductKeywords.Remove(websiteProductKeyword);
                        await _websiteDbContext.SaveChangesAsync();
                    }
                }

            }

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}