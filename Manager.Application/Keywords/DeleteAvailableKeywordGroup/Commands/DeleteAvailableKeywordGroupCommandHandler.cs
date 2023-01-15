using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Keywords.DeleteAvailableKeywordGroup.Commands
{
    public sealed class DeleteAvailableKeywordGroupCommandHandler : IRequestHandler<DeleteAvailableKeywordGroupCommand, Result>
    {
        private readonly IManagerDbContext _managerDbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public DeleteAvailableKeywordGroupCommandHandler(IManagerDbContext dbContext, IWebsiteDbContext websiteDbContext)
        {
            _managerDbContext = dbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(DeleteAvailableKeywordGroupCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.KeywordGroup keywordGroup = await _managerDbContext.KeywordGroups
                .Where(x => x.Id == request.KeywordGroupId)
                .Include(x => x.KeywordsInKeywordGroup)
                .SingleAsync();

            List<Domain.Entities.Keyword> keywords = await _managerDbContext.Keywords
                .Where(x => keywordGroup.KeywordsInKeywordGroup
                    .Select(z => z.KeywordId)
                    .Contains(x.Id) && x.KeywordsInKeywordGroup.Count == 1)
                .ToListAsync();

            List<Domain.Entities.ProductKeyword> productKeywords = await _managerDbContext.ProductKeywords
                .Where(x => keywordGroup.KeywordsInKeywordGroup
                    .Select(z => z.KeywordId)
                    .Contains(x.KeywordId) && 
                        x.Keyword.KeywordsInKeywordGroup.Count > 1 && 
                        x.Product.KeywordGroupsBelongingToProduct.Count == 1)
                .ToListAsync();

            _managerDbContext.Keywords.RemoveRange(keywords);
            _managerDbContext.ProductKeywords.RemoveRange(productKeywords);
            _managerDbContext.KeywordGroups.Remove(keywordGroup);




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
            await _managerDbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}