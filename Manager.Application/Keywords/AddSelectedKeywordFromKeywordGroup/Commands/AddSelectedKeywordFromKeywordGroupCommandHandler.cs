using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.AddSelectedKeywordFromKeywordGroup.Commands
{
    public sealed class AddSelectedKeywordFromKeywordGroupCommandHandler : IRequestHandler<AddSelectedKeywordFromKeywordGroupCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public AddSelectedKeywordFromKeywordGroupCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(AddSelectedKeywordFromKeywordGroupCommand request, CancellationToken cancellationToken)
        {
            KeywordGroupBelongingToProduct keywordGroupBelongingToProduct = KeywordGroupBelongingToProduct.Create(request.KeywordGroupId, request.ProductId);
            _dbContext.KeywordGroupsBelongingToProduct.Add(keywordGroupBelongingToProduct);

            if (await _dbContext.ProductKeywords.CountAsync(x => x.KeywordId == request.KeywordId && x.ProductId == request.ProductId) == 0)
            {
                ProductKeyword productKeyword = ProductKeyword.Create(request.ProductId, request.KeywordId);
                _dbContext.ProductKeywords.Add(productKeyword);

                string userId = _authService.GetUserIdFromClaims();
                productKeyword.AddDomainEvent(new ProductModifiedEvent(request.ProductId, userId));
            }

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}