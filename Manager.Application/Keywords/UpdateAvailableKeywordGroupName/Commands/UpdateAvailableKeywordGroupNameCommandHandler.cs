using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.UpdateAvailableKeywordGroupName.Commands
{
    public sealed class UpdateAvailableKeywordGroupNameCommandHandler : IRequestHandler<UpdateAvailableKeywordGroupNameCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public UpdateAvailableKeywordGroupNameCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdateAvailableKeywordGroupNameCommand request, CancellationToken cancellationToken)
        {
            KeywordGroup keywordGroup = (await _dbContext.KeywordGroups.FindAsync(request.Id))!;
            keywordGroup.EditName(request.Name);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}