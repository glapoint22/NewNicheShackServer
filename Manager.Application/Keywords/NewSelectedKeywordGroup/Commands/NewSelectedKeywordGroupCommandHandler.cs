using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.NewSelectedKeywordGroup.Commands
{
    public sealed class NewSelectedKeywordGroupCommandHandler : IRequestHandler<NewSelectedKeywordGroupCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public NewSelectedKeywordGroupCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(NewSelectedKeywordGroupCommand request, CancellationToken cancellationToken)
        {
            KeywordGroup keywordGroup = KeywordGroup.Create(request.Name, true);

            KeywordGroupBelongingToProduct keywordGroupBelongingToProduct = KeywordGroupBelongingToProduct.Create(keywordGroup, request.ProductId);
            _dbContext.KeywordGroupsBelongingToProduct.Add(keywordGroupBelongingToProduct);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded(keywordGroup.Id);
        }
    }
}