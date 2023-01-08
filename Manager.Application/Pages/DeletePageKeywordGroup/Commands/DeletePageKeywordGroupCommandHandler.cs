using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Pages.DeletePageKeywordGroup.Commands
{
    public sealed class DeletePageKeywordGroupCommandHandler : IRequestHandler<DeletePageKeywordGroupCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public DeletePageKeywordGroupCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
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

            string userId = _authService.GetUserIdFromClaims();
            page.AddDomainEvent(new PageModifiedEvent(page.Id, userId));


            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}