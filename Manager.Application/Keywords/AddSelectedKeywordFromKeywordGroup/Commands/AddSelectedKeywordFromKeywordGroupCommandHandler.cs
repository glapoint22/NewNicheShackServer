using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.AddSelectedKeywordFromKeywordGroup.Commands
{
    public sealed class AddSelectedKeywordFromKeywordGroupCommandHandler : IRequestHandler<AddSelectedKeywordFromKeywordGroupCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public AddSelectedKeywordFromKeywordGroupCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddSelectedKeywordFromKeywordGroupCommand request, CancellationToken cancellationToken)
        {
            KeywordGroupBelongingToProduct keywordGroupBelongingToProduct = KeywordGroupBelongingToProduct.Create(request.KeywordGroupId, request.ProductId);
            _dbContext.KeywordGroupsBelongingToProduct.Add(keywordGroupBelongingToProduct);

            if (await _dbContext.ProductKeywords.CountAsync(x => x.KeywordId == request.KeywordId && x.ProductId == request.ProductId) == 0)
            {
                ProductKeyword productKeyword = ProductKeyword.Create(request.ProductId, request.KeywordId);
                _dbContext.ProductKeywords.Add(productKeyword);
            }

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}