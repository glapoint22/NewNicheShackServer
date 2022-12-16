using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.UpdateKeywordGroupName.Commands
{
    public sealed class UpdateKeywordGroupNameCommandHandler : IRequestHandler<UpdateKeywordGroupNameCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public UpdateKeywordGroupNameCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdateKeywordGroupNameCommand request, CancellationToken cancellationToken)
        {
            KeywordGroup keywordGroup = (await _dbContext.KeywordGroups.FindAsync(request.Id))!;
            keywordGroup.EditName(request.Name);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}