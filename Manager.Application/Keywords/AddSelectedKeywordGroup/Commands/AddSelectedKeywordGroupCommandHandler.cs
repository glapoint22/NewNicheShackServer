using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.AddSelectedKeywordGroup.Commands
{
    public sealed class AddSelectedKeywordGroupCommandHandler : IRequestHandler<AddSelectedKeywordGroupCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public AddSelectedKeywordGroupCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(AddSelectedKeywordGroupCommand request, CancellationToken cancellationToken)
        {
            var keywordGroup = await _dbContext.KeywordGroups
                .Where(x => x.Id == request.KeywordGroupId)
                .Include(x => x.KeywordsInKeywordGroup
                    .Where(z => !z.Keyword.ProductKeywords
                        .Any(q => q.ProductId == request.ProductId)))
                .SingleAsync();

            // Add the keyword group to the product
            KeywordGroupBelongingToProduct keywordGroupBelongingToProduct = KeywordGroupBelongingToProduct.Create(request.KeywordGroupId, request.ProductId);
            _dbContext.KeywordGroupsBelongingToProduct.Add(keywordGroupBelongingToProduct);

            // Add the keywords to the product
            _dbContext.ProductKeywords.AddRange(keywordGroup.KeywordsInKeywordGroup.Select(x => new ProductKeyword
            {
                KeywordId = x.KeywordId,
                ProductId = request.ProductId
            }));

            string userId = _authService.GetUserIdFromClaims();
            keywordGroup.AddDomainEvent(new ProductModifiedEvent(request.ProductId, userId));

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}