using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Keywords.AddAvailableKeywords.Commands
{
    public sealed class AddAvailableKeywordsCommandHandler : IRequestHandler<AddAvailableKeywordsCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public AddAvailableKeywordsCommandHandler(IManagerDbContext dbContext, IWebsiteDbContext websiteDbContext)
        {
            _dbContext = dbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(AddAvailableKeywordsCommand request, CancellationToken cancellationToken)
        {
            // See if any of these keywords exists in the system
            var keywords = await _dbContext.Keywords
                .Where(x => request.Keywords.Contains(x.Name))
                .ToListAsync();

            // Create the keywords we don't have yet
            foreach (var keywordName in request.Keywords)
            {
                if (!keywords.Select(x => x.Name).Contains(keywordName))
                {
                    keywords.Add(Keyword.Create(keywordName));
                }
            }

            // Add these keywords to the group
            _dbContext.KeywordsInKeywordGroup
                .AddRange(keywords
                    .Select(x => KeywordInKeywordGroup.Create(x, request.KeywordGroupId)));




            // Add these keywords to products if products contain this keyword group
            var products = await _dbContext.KeywordGroupsBelongingToProduct
                .Where(x => x.KeywordGroupId == request.KeywordGroupId)
                .Select(x => new
                {
                    x.ProductId,
                    x.Product.ProductKeywords
                }).ToListAsync();


            // If we have products
            if (products.Count > 0)
            {
                foreach (var product in products)
                {
                    // Add the keywords to the products
                    _dbContext.ProductKeywords.AddRange(keywords.Where(x => !product.ProductKeywords.Select(z => z.KeywordId).Contains(x.Id))
                        .Select(x => ProductKeyword.Create(product.ProductId, x)).ToList());
                }


                // See if website has these products
                List<Guid> websiteProductIds = await _websiteDbContext.Products
                    .Where(x => products.Select(z => z.ProductId).Contains(x.Id))
                    .Select(x => x.Id)
                    .ToListAsync();


                // If website has these products
                if (websiteProductIds.Count > 0)
                {
                    foreach (var product in products)
                    {
                        // Add the keywords to the products
                        _websiteDbContext.ProductKeywords.AddRange(keywords.Where(x => websiteProductIds.Contains(product.ProductId) && !product.ProductKeywords.Select(z => z.KeywordId).Contains(x.Id))
                        .Select(x => new Website.Domain.Entities.ProductKeyword
                        {
                            ProductId = product.ProductId,
                            KeywordId = x.Id
                        }));
                    }

                    await _websiteDbContext.SaveChangesAsync();
                }
            }


            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(keywords.Select(x => new
            {
                x.Id,
                x.Name
            }));
        }
    }
}