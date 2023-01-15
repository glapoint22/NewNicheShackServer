using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Keywords.DeleteAvailableKeyword.Commands
{
    public sealed class DeleteAvailableKeywordCommandHandler : IRequestHandler<DeleteAvailableKeywordCommand, Result>
    {
        private readonly IManagerDbContext _managerDbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public DeleteAvailableKeywordCommandHandler(IManagerDbContext dbContext, IWebsiteDbContext websiteDbContext)
        {
            _managerDbContext = dbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(DeleteAvailableKeywordCommand request, CancellationToken cancellationToken)
        {
            int count = await _managerDbContext.KeywordsInKeywordGroup
                .CountAsync(x => x.KeywordId == request.KeywordId);


            Domain.Entities.KeywordInKeywordGroup keywordInKeywordGroup = await _managerDbContext.KeywordsInKeywordGroup
                .Where(x => x.KeywordId == request.KeywordId && x.KeywordGroupId == request.KeywordGroupId)
                .Include(x => x.Keyword)
                .SingleAsync();

            _managerDbContext.KeywordsInKeywordGroup.Remove(keywordInKeywordGroup);

            if (count == 1)
            {
                _managerDbContext.Keywords.Remove(keywordInKeywordGroup.Keyword);

                Website.Domain.Entities.Keyword? keyword = await _websiteDbContext.Keywords.FindAsync(request.KeywordId);
                if (keyword != null)
                {
                    _websiteDbContext.Keywords.Remove(keyword);
                    await _websiteDbContext.SaveChangesAsync();
                }
            }
            else
            {
                List<Domain.Entities.ProductKeyword> managerProductKeywords = await _managerDbContext.ProductKeywords
                    .Where(x => x.KeywordId == request.KeywordId &&
                        x.Product.KeywordGroupsBelongingToProduct.Count == 1)
                    .ToListAsync();

                _managerDbContext.ProductKeywords.RemoveRange(managerProductKeywords);

                List<Website.Domain.Entities.ProductKeyword> websiteProductKeywords = await _websiteDbContext.ProductKeywords
                    .Where(x => managerProductKeywords
                        .Select(z => z.KeywordId)
                        .Contains(x.KeywordId) &&
                            managerProductKeywords
                                .Select(z => z.ProductId)
                                .Contains(x.ProductId))
                    .ToListAsync();

                _websiteDbContext.ProductKeywords.RemoveRange(websiteProductKeywords);
                await _websiteDbContext.SaveChangesAsync();
            }

            await _managerDbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}