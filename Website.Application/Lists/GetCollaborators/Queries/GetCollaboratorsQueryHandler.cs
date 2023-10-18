using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.Lists.GetCollaborators.Queries
{
    public sealed class GetCollaboratorsQueryHandler : IRequestHandler<GetCollaboratorsQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IAuthService _authService;

        public GetCollaboratorsQueryHandler(IWebsiteDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(GetCollaboratorsQuery request, CancellationToken cancellationToken)
        {
            string userId = _authService.GetUserIdFromClaims();

            // Get all collaborators from the list except the owner and the current user
            var collaborators = await _dbContext.Collaborators
                .Where(x => x.ListId == request.ListId && !x.IsOwner)
                .Select(x => new
                {
                    x.Id,
                    Name = x.User.FirstName + " " + x.User.LastName,
                    ListPermissions = new
                    {
                        x.CanAddToList,
                        x.CanShareList,
                        x.CanEditList,
                        x.CanInviteCollaborators,
                        x.CanManageCollaborators,
                        x.CanDeleteList,
                        x.CanRemoveFromList
                    }
                })
                .ToListAsync(cancellationToken: cancellationToken);

            return Result.Succeeded(collaborators);
        }
    }
}