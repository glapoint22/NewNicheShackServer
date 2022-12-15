using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.AddAvailableKeyword.Commands
{
    public sealed class AddAvailableKeywordCommandHandler : IRequestHandler<AddAvailableKeywordCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public AddAvailableKeywordCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
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
            var productIds = await _dbContext.KeywordGroupsBelongingToProduct
                .Where(x => x.KeywordGroupId == request.Id && !x.Product.ProductKeywords
                    .Any(z => z.KeywordId == keyword.Id))
                .Select(x => x.ProductId)
                .ToListAsync();

            if (productIds.Count > 0)
            {
                _dbContext.ProductKeywords
                    .AddRange(productIds.Select(x => ProductKeyword.Create(x, keyword)));
            }

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded(keyword.Id);
        }
    }
}