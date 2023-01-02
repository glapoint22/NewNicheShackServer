using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Notifications.GetBlockedUsers.Queries
{
    public sealed class GetBlockedUsersQueryHandler : IRequestHandler<GetBlockedUsersQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public GetBlockedUsersQueryHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetBlockedUsersQuery request, CancellationToken cancellationToken)
        {
            var blockedUsers = await _dbContext.Users
                .Where(x => x.BlockNotificationSending)
                .Select(x => new
                {
                    UserId = x.Id,
                    Email = "",
                    Name = x.FirstName + " " + x.LastName
                }).ToListAsync();

            var blockedNonAccountEmails = await _dbContext.BlockedNonAccountEmails
                .Select(x => new
                {
                    UserId = "",
                    x.Email,
                    x.Name
                }).ToListAsync();

            return Result.Succeeded(blockedUsers
                .Concat(blockedNonAccountEmails)
                .OrderBy(x => x.Name));
        }
    }
}