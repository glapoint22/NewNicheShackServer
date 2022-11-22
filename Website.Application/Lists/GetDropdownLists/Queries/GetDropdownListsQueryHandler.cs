using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.Lists.GetDropdownLists.Queries
{
    public sealed class GetDropdownListsQueryHandler : IRequestHandler<GetDropdownListsQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IAuthService _authService;

        public GetDropdownListsQueryHandler(IWebsiteDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(GetDropdownListsQuery request, CancellationToken cancellationToken)
        {
            string userId = _authService.GetUserIdFromClaims();

            var lists = await _dbContext.Collaborators
                .Where(x => x.UserId == userId && (x.IsOwner || x.CanAddToList))
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