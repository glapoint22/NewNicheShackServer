using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.AddAvailableKeywordGroup.Commands
{
    public sealed class AddAvailableKeywordGroupCommandHandler : IRequestHandler<AddAvailableKeywordGroupCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public AddAvailableKeywordGroupCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddAvailableKeywordGroupCommand request, CancellationToken cancellationToken)
        {
            KeywordGroup keywordGroup = KeywordGroup.Create(request.Name);

            _dbContext.KeywordGroups.Add(keywordGroup);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(keywordGroup.Id);
        }
    }
}