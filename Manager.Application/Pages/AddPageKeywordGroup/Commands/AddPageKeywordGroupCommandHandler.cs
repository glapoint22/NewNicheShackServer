using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Pages.AddPageKeywordGroup.Commands
{
    public sealed class AddPageKeywordGroupCommandHandler : IRequestHandler<AddPageKeywordGroupCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public AddPageKeywordGroupCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
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

            string userId = _authService.GetUserIdFromClaims();
            page.AddDomainEvent(new PageModifiedEvent(page.Id, userId));

            await _dbContext.SaveChangesAsync();


            return Result.Succeeded();
        }
    }
}