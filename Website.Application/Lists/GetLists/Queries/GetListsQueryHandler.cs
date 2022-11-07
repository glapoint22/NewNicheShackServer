using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Entities;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.Common;
using Website.Application.Lists.UpdateCollaborators.Classes;

namespace Website.Application.Lists.GetLists.Queries
{
    public sealed class GetListsQueryHandler : IRequestHandler<GetListsQuery, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public GetListsQueryHandler(IUserService userService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetListsQuery request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();

            List<ListDto> lists = await _dbContext.Lists
                .Where(x => x.Collaborators
                    .Where(y => y.UserId == user.Id)
                    .Any())
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Description,
                    TotalProducts = x.Products.Count,
                    x.CollaborateId,
                    CollaboratorCount = x.Collaborators
                        .Count(z => !z.IsOwner && z.UserId != user.Id),
                    CollaboratorId = x.Collaborators
                        .Where(z => z.UserId == user.Id && z.ListId == x.Id)
                        .Select(z => z.Id)
                        .Single(),
                    ListPermissions = x.Collaborators
                        .Where(z => z.UserId == user.Id)
                        .Select(z => new ListPermissions
                        {
                            CanAddToList = z.CanAddToList,
                            CanShareList = z.CanShareList,
                            CanEditList = z.CanEditList,
                            CanInviteCollaborators = z.CanInviteCollaborators,
                            CanDeleteList = z.CanDeleteList,
                            CanRemoveFromList = z.CanRemoveFromList,
                            CanManageCollaborators = z.CanManageCollaborators
                        })
                        .Single(),
                    Owner = x.Collaborators
                        .Where(z => z.IsOwner)
                        .Select(z => z.User)
                        .Single()
                })
                .Select(x => new ListDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description!,
                    TotalProducts = x.TotalProducts,
                    CollaborateId = x.CollaborateId,
                    CollaboratorCount = x.CollaboratorCount,
                    CollaboratorId = x.CollaboratorId,
                    ListPermissions = x.ListPermissions,
                    OwnerName = x.Owner.Id == user.Id ? "You" : x.Owner.FirstName,
                    IsOwner = x.Owner.Id == user.Id,
                    OwnerProfileImage = new Image
                    {
                        Name = x.Owner.FirstName,
                        Src = x.Owner.Image!
                    }
                })
                .ToListAsync(cancellationToken: cancellationToken);


            string listId = request.ListId ?? lists.Select(x => x.Id).First();


            List<ListProductDto> products = await _dbContext.ListProducts
                .SortBy(request.Sort)
                .Where(x => x.ListId == listId)
                .Select(user)
                .ToListAsync();

            return Result.Succeeded(new
            {
                lists,
                products
            });
        }
    }
}