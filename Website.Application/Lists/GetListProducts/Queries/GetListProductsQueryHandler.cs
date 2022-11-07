using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Entities;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.Common;

namespace Website.Application.Lists.GetListProducts.Queries
{
    public sealed class GetListProductsQueryHandler : IRequestHandler<GetListProductsQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IUserService _userService;

        public GetListProductsQueryHandler(IWebsiteDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result> Handle(GetListProductsQuery request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();

            List<ListProductDto> products = await _dbContext.ListProducts
                .SortBy(request.Sort)
                .Where(x => x.ListId == request.ListId)
                .Select(user)
                .ToListAsync();

            return Result.Succeeded(products);
        }
    }
}