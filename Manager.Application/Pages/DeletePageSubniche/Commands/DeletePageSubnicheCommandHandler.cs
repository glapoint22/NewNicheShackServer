using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Pages.DeletePageSubniche.Commands
{
    public sealed class DeletePageSubnicheCommandHandler : IRequestHandler<DeletePageSubnicheCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public DeletePageSubnicheCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(DeletePageSubnicheCommand request, CancellationToken cancellationToken)
        {
            PageSubniche pageSubniche = await _dbContext.PageSubniches
                .Where(x => x.PageId == request.PageId && x.SubnicheId == request.SubnicheId)
                .SingleAsync();

            _dbContext.PageSubniches.Remove(pageSubniche);


            string userId = _authService.GetUserIdFromClaims();
            pageSubniche.AddDomainEvent(new PageModifiedEvent(request.PageId, userId));

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}