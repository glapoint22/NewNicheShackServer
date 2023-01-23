using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Keywords.AddAvailableKeyword.Commands
{
    public sealed class AddAvailableKeywordCommandHandler : IRequestHandler<AddAvailableKeywordCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public AddAvailableKeywordCommandHandler(IManagerDbContext dbContext, IWebsiteDbContext websiteDbContext)
        {
            _dbContext = dbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(AddAvailableKeywordCommand request, CancellationToken cancellationToken)
        {
            Keyword? keyword = await _dbContext.Keywords
                .Where(x => x.Name == request.Name)
                .SingleOrDefaultAsync();

            keyword ??= Keyword.Create(request.Name);

            KeywordInKeywordGroup keywordInKeywordGroup = KeywordInKeywordGroup.Create(keyword, request.Id);
            _dbContext.KeywordsInKeywordGroup.Add(keywordInKeywordGroup);



            // Add this keyword to other products if other products contain this keyword group
            List<Guid> productIds = await _dbContext.KeywordGroupsBelongingToProduct
                .Where(x => x.KeywordGroupId == request.Id && !x.Product.ProductKeywords
                    .Any(z => z.KeywordId == keyword.Id))
                .Select(x => x.ProductId)
                .ToListAsync();

            // If we have products that contain this keyword group
            if (productIds.Count > 0)
            {
                // Add the keyword to the product
                _dbContext.ProductKeywords
                    .AddRange(productIds.Select(x => ProductKeyword.Create(x, keyword)));

                // See if website has these products
                List<Guid> websiteProductIds = await _websiteDbContext.Products
                    .Where(x => productIds.Contains(x.Id))
                    .Select(x => x.Id)
                    .ToListAsync();

                // If website has these products
                if (websiteProductIds.Count > 0)
                {
                    // Add the keyword to the product
                    _websiteDbContext.ProductKeywords
                    .AddRange(websiteProductIds.Select(x => new Website.Domain.Entities.ProductKeyword
                    {
                        ProductId = x,
                        KeywordId = keyword.Id
                    }));

                    await _websiteDbContext.SaveChangesAsync();
                }
            }

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded(keyword.Id);
        }
    }
}