using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Pages.AddPageSubniche.Commands
{
    public sealed class AddPageSubnicheCommandHandler : IRequestHandler<AddPageSubnicheCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public AddPageSubnicheCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(AddPageSubnicheCommand request, CancellationToken cancellationToken)
        {
            Page page = await _dbContext.Pages
                .Where(x => x.Id == request.PageId)
                .Include(x => x.PageSubniches)
                .SingleAsync();

            page.AddPageSubniche(request.SubnicheId);

            string userId = _authService.GetUserIdFromClaims();
            page.AddDomainEvent(new PageModifiedEvent(page.Id, userId));

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}