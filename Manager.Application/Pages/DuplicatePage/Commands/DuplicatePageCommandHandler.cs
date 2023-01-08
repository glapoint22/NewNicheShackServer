using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Pages.DuplicatePage.Commands
{
    public sealed class DuplicatePageCommandHandler : IRequestHandler<DuplicatePageCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public DuplicatePageCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(DuplicatePageCommand request, CancellationToken cancellationToken)
        {
            string userId = _authService.GetUserIdFromClaims();

            Page page = await _dbContext.Pages
                .Where(x => x.Id == request.Id)
                .Include(x => x.PageSubniches)
                .Include(x => x.PageKeywordGroups)
                    .ThenInclude(x => x.KeywordGroup)
                    .ThenInclude(x => x.KeywordsInKeywordGroup)
                .SingleAsync();

            Page duplicatePage = page.Duplicate();
            _dbContext.Pages.Add(duplicatePage);


            duplicatePage.AddDomainEvent(new PageCreatedEvent(duplicatePage.Id, userId));
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(duplicatePage.Id);
        }
    }
}