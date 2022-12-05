using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Pages.AddPageKeywordGroup.Commands
{
    public sealed class AddPageKeywordGroupCommandHandler : IRequestHandler<AddPageKeywordGroupCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public AddPageKeywordGroupCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddPageKeywordGroupCommand request, CancellationToken cancellationToken)
        {
            Page page = await _dbContext.Pages
                .Where(x => x.Id == request.PageId)
                .Include(x => x.PageKeywordGroups)
                .SingleAsync();

            KeywordGroup keywordGroup = await _dbContext.KeywordGroups
                .Where(x => x.Id == request.KeywordGroupId)
                .Include(x => x.KeywordsInKeywordGroup)
                .SingleAsync();

            page.AddPageKeywordGroup(keywordGroup);
            await _dbContext.SaveChangesAsync();


            return Result.Succeeded();
        }
    }
}