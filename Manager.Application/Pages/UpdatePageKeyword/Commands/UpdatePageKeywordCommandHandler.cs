using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Pages.UpdatePageKeyword.Commands
{
    public sealed class UpdatePageKeywordCommandHandler : IRequestHandler<UpdatePageKeywordCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public UpdatePageKeywordCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdatePageKeywordCommand request, CancellationToken cancellationToken)
        {
            var keywordInKeywordGroupId = await _dbContext.KeywordsInKeywordGroup
                .Where(x => x.KeywordGroupId == request.KeywordGroupId && x.KeywordId == request.KeywordId)
                .Select(x => x.Id)
                .SingleAsync();

            Page page = await _dbContext.Pages
                .Where(x => x.Id == request.PageId)
                .Include(x => x.PageKeywords)
                .SingleAsync();

            if (!request.Checked)
            {
                page.RemovePageKeyword(keywordInKeywordGroupId);
            }
            else
            {
                page.AddPageKeyword(keywordInKeywordGroupId);
            }


            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}