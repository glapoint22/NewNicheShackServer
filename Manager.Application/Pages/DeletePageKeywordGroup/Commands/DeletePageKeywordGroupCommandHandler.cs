using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Pages.DeletePageKeywordGroup.Commands
{
    public sealed class DeletePageKeywordGroupCommandHandler : IRequestHandler<DeletePageKeywordGroupCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DeletePageKeywordGroupCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeletePageKeywordGroupCommand request, CancellationToken cancellationToken)
        {
            Page page = await _dbContext.Pages
                .Where(x => x.Id == request.PageId)
                .Include(x => x.PageKeywordGroups
                    .Where(z => z.KeywordGroupId == request.KeywordGroupId))
                .Include(x => x.PageKeywords)
                .SingleAsync();

            page.RemovePageKeywordGroup();


            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}