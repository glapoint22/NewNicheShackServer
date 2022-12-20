using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Pages.DuplicatePage.Commands
{
    public sealed class DuplicatePageCommandHandler : IRequestHandler<DuplicatePageCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DuplicatePageCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DuplicatePageCommand request, CancellationToken cancellationToken)
        {
            Page page = await _dbContext.Pages
                .Where(x => x.Id == request.Id)
                .Include(x => x.PageSubniches)
                .Include(x => x.PageKeywordGroups)
                    .ThenInclude(x => x.KeywordGroup)
                    .ThenInclude(x => x.KeywordsInKeywordGroup)
                .SingleAsync();

            Page duplicatePage = page.Duplicate();
            _dbContext.Pages.Add(duplicatePage);


            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(duplicatePage.Id);
        }
    }
}