using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.Common;
using Website.Domain.Entities;

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

            User user = await _userService.GetUserFromClaimsAsync();

            List<ListProductDto> products = await _dbContext.ListProducts
                .SortBy(request.Sort)
                .Where(x => x.ListId == request.ListId)
                .Select(user)
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