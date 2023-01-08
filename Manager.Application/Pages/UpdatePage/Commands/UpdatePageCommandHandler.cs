using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Pages.UpdatePage.Commands
{
    public sealed class UpdatePageCommandHandler : IRequestHandler<UpdatePageCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public UpdatePageCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(UpdatePageCommand request, CancellationToken cancellationToken)
        {
            Page page = await _dbContext.Pages
                .Where(x => x.Id == request.Id)
                .Include(x => x.PageSubniches)
                .Include(x => x.PageKeywordGroups)
                .SingleAsync();

            page.Update(request.Name, request.Content, request.PageType);

            string userId = _authService.GetUserIdFromClaims();
            page.AddDomainEvent(new PageModifiedEvent(page.Id, userId));

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}