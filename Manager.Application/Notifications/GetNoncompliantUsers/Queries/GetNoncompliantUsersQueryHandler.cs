using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Notifications.GetNoncompliantUsers.Queries
{
    public sealed class GetNoncompliantUsersQueryHandler : IRequestHandler<GetNoncompliantUsersQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public GetNoncompliantUsersQueryHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetNoncompliantUsersQuery request, CancellationToken cancellationToken)
        {
            var noncompliantUsers = await _dbContext.Users
                .Where(x => x.NoncompliantStrikes > 0)
                .Select(x => new
                {
                    Name = x.FirstName + " " + x.LastName,
                    Strikes = x.NoncompliantStrikes.ToString()
                }).ToListAsync();

            return Result.Succeeded(noncompliantUsers);
        }
    }
}