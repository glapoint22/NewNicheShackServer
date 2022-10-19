﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.Lists.DropdownLists.Queries
{
    public class GetDropdownListsQueryHandler : IRequestHandler<GetDropdownListsQuery, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public GetDropdownListsQueryHandler(IUserService userService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetDropdownListsQuery request, CancellationToken cancellationToken)
        {
            string userId = _userService.GetUserIdFromClaims();

            var lists = await _dbContext.Collaborators
                .Where(x => x.UserId == userId && x.CanAddToList)
                .Select(x => new
                {
                    Key = x.List.Name,
                    Value = x.ListId
                })
                .ToListAsync(cancellationToken: cancellationToken);

            return Result.Succeeded(lists);
        }
    }
}