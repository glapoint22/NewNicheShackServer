using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Entities;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.Common;

namespace Website.Application.Lists.GetSharedList.Queries
{
    public sealed class GetSharedListQueryHandler : IRequestHandler<GetSharedListQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IUserService _userService;

        public GetSharedListQueryHandler(IWebsiteDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result> Handle(GetSharedListQuery request, CancellationToken cancellationToken)
        {
            string? listName = await _dbContext.Lists
                .Where(x => x.Id == request.ListId)
                .Select(x => x.Name)
                .FirstOrDefaultAsync();

            if (listName == null) return Result.Failed("404");

            string? userTrackingCode = null;
            User user = await _userService.GetUserFromClaimsAsync();

            if (user != null) userTrackingCode = user.TrackingCode;

            List<ListProductDto> products = await _dbContext.ListProducts
                .SortBy(request.Sort)
                .Where(x => x.ListId == request.ListId)
                .Select(userTrackingCode)
                .ToListAsync();



            return Result.Succeeded(new
            {
                request.ListId,
                Name = listName,
                Products = products
            });
        }
    }
}