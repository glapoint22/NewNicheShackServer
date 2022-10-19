﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.Lists.ListCollection.Queries
{
    public class GetListCollectionQueryHandler : IRequestHandler<GetListCollectionQuery, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public GetListCollectionQueryHandler(IUserService userService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetListCollectionQuery request, CancellationToken cancellationToken)
        {
            string userId = _userService.GetUserIdFromClaims();

            var lists = await _dbContext.Lists
                .Where(x => x.Collaborators
                    .Where(y => y.UserId == userId)
                    .Any())
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Description,
                    TotalProducts = x.Collaborators
                        .SelectMany(z => z.CollaboratorProducts)
                        .Count(),
                    x.CollaborateId,
                    CollaboratorCount = x.Collaborators
                        .Count(z => !z.IsOwner && z.UserId != userId),
                    ListPermissions = x.Collaborators
                        .Where(z => z.UserId == userId)
                        .Select(z => new
                        {
                            z.CanAddToList,
                            z.CanShareList,
                            z.CanEditList,
                            z.CanInviteCollaborators,
                            z.CanDeleteList,
                            z.CanMoveItem,
                            z.CanRemoveItem,
                            z.CanManageCollaborators
                        })
                        .Single(),
                    Owner = x.Collaborators
                        .Where(z => z.IsOwner)
                        .Select(z => z.User)
                        .Single()
                })
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Description,
                    x.TotalProducts,
                    x.CollaborateId,
                    x.CollaboratorCount,
                    x.ListPermissions,
                    OwnerName = x.Owner.FirstName,
                    IsOwner = x.Owner.Id == userId,
                    OwnerProfileImage = new
                    {
                        Name = x.Owner.FirstName,
                        Src = x.Owner.Image
                    }
                })
                .ToListAsync(cancellationToken: cancellationToken);

            return Result.Succeeded(lists);
        }
    }
}