using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Lists.GetListInfo.Queries
{
    public class GetListInfoQueryHandler : IRequestHandler<GetListInfoQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IUserService _userService;

        public GetListInfoQueryHandler(IWebsiteDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result> Handle(GetListInfoQuery request, CancellationToken cancellationToken)
        {
            Collaborator listOwner = await _dbContext.Collaborators
                .Where(x => x.List.CollaborateId == request.CollaborateId)
                .Include(x => x.List)
                .Include(x => x.User)
                .FirstAsync(cancellationToken: cancellationToken);

            if (await _dbContext.Collaborators
                .AnyAsync(x => x.UserId == _userService.GetUserIdFromClaims() && x.ListId == listOwner.ListId))
            {
                return Result.Succeeded();
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