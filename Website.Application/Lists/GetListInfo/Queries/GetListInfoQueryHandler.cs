using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Lists.GetListInfo.Queries
{
    public sealed class GetListInfoQueryHandler : IRequestHandler<GetListInfoQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IAuthService _authService;

        public GetListInfoQueryHandler(IWebsiteDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(GetListInfoQuery request, CancellationToken cancellationToken)
        {
            Collaborator? listOwner = await _dbContext.Collaborators
                .Where(x => x.List.CollaborateId == request.CollaborateId)
                .Include(x => x.List)
                .Include(x => x.User)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (listOwner == null) return Result.Failed("404");

            // If user is already collaborating on this list
            if (await _dbContext.Collaborators
                .AnyAsync(x => x.UserId == _authService.GetUserIdFromClaims() && x.ListId == listOwner.ListId))
            {
                return Result.Succeeded(new
                {
                    isCollaborator = true,
                    listId = listOwner.ListId
                });
            }


            return Result.Succeeded(new
            {
                OwnerName = listOwner.User.FirstName + " " + listOwner.User.LastName,
                ProfileImage = listOwner.User.Image,
                listOwner.ListId,
                ListName = listOwner.List.Name
            });
        }
    }
}