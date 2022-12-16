using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.UpdateKeywordName.Commands
{
    public sealed class UpdateKeywordNameCommandHandler : IRequestHandler<UpdateKeywordNameCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public UpdateKeywordNameCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdateKeywordNameCommand request, CancellationToken cancellationToken)
        {
            Keyword? keyword = await _dbContext.Keywords
                .Where(x => x.Name == request.Name.Trim().ToLower())
                .SingleOrDefaultAsync();

            if (keyword != null)
            {
                // Remove the old keyword
                Keyword oldKeyword = (await _dbContext.Keywords.FindAsync(request.Id))!;
                _dbContext.Keywords.Remove(oldKeyword);

                // Get all the keyword groups the old keyword was in
                var keywordGroupIds = await _dbContext.KeywordsInKeywordGroup
                    .Where(x => x.KeywordId == oldKeyword.Id)
                    .Select(x => x.KeywordGroupId)
                    .ToListAsync();

                // Add the keyword to the groups
                _dbContext.KeywordsInKeywordGroup
                    .AddRange(keywordGroupIds
                        .Select(keywordGroupId => KeywordInKeywordGroup.Create(keyword, keywordGroupId)));


                // Get all the products the old keyword was part of
                var productIds = await _dbContext.ProductKeywords
                    .Where(x => x.KeywordId == oldKeyword.Id && x.Product.KeywordGroupsBelongingToProduct
                        .Count(z => z.KeywordGroup.KeywordsInKeywordGroup
                            .Any(q => q.KeywordId == keyword.Id)) == 0)
                    .Select(x => x.ProductId)
                    .ToListAsync();

                // Add the keyword to the product
                if (productIds.Count > 0)
                {
                    _dbContext.ProductKeywords
                        .AddRange(productIds.Select(x => ProductKeyword.Create(x, keyword)));
                }
            }
            else
            {
                keyword = (await _dbContext.Keywords.FindAsync(request.Id))!;
                keyword.UpdateName(request.Name);
            }


            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}