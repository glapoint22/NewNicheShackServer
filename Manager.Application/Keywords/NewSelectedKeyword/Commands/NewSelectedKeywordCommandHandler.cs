using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.NewSelectedKeyword.Commands
{
    public sealed class NewSelectedKeywordCommandHandler : IRequestHandler<NewSelectedKeywordCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public NewSelectedKeywordCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(NewSelectedKeywordCommand request, CancellationToken cancellationToken)
        {
            Keyword? keyword = await _dbContext.Keywords
                .Where(x => x.Name == request.Name)
                .SingleOrDefaultAsync();

            keyword ??= Keyword.Create(request.Name);

            KeywordInKeywordGroup keywordInKeywordGroup = KeywordInKeywordGroup.Create(keyword, request.KeywordGroupId);
            _dbContext.KeywordsInKeywordGroup.Add(keywordInKeywordGroup);

            if (!await _dbContext.ProductKeywords.AnyAsync(x => x.KeywordId == keyword.Id && x.ProductId == request.ProductId))
            {
                ProductKeyword productKeyword = ProductKeyword.Create(request.ProductId, keyword);
                _dbContext.ProductKeywords.Add(productKeyword);

                string userId = _authService.GetUserIdFromClaims();
                productKeyword.AddDomainEvent(new ProductModifiedEvent(request.ProductId, userId));
            }

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded(keyword.Id);
        }
    }
}