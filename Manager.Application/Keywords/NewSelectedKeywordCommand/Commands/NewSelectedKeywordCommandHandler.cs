using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.NewSelectedKeywordCommand.Commands
{
    public sealed class NewSelectedKeywordCommandHandler : IRequestHandler<NewSelectedKeywordCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public NewSelectedKeywordCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
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
            }

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded(keyword.Id);
        }
    }
}