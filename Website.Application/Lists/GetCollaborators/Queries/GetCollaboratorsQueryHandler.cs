using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.Lists.GetCollaborators.Queries
{
    public sealed class GetCollaboratorsQueryHandler : IRequestHandler<GetCollaboratorsQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IUserService _userService;

        public GetCollaboratorsQueryHandler(IWebsiteDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result> Handle(GetCollaboratorsQuery request, CancellationToken cancellationToken)
        {
            string userId = _userService.GetUserIdFromClaims();

            // Get all collaborators from the list except the owner and the current user
            var collaborators = await _dbContext.Collaborators
                .Where(x => x.ListId == request.ListId && !x.IsOwner && x.UserId != userId)
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
                        x.CanDeleteList,
                        x.CanRemoveItem,
                        x.CanManageCollaborators
                    }
                })
                .ToListAsync(cancellationToken: cancellationToken);

            return Result.Succeeded(collaborators);
        }
    }
}