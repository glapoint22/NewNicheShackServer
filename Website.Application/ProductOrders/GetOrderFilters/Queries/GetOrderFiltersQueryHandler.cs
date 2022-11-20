using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.ProductOrders.GetOrderFilters.Queries
{
    public sealed class GetOrderFiltersQueryHandler : IRequestHandler<GetOrderFiltersQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IUserService _userService;

        public GetOrderFiltersQueryHandler(IWebsiteDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result> Handle(GetOrderFiltersQuery request, CancellationToken cancellationToken)
        {
            string userId = _userService.GetUserIdFromClaims();

            List<KeyValuePair<string, string>> filterOptions = new()
            {
                new KeyValuePair<string, string>("Last 30 days", "last-30"),
                new KeyValuePair<string, string>("Past 6 months", "6-months"),
            };

            // Get years when products were bought from this user
            List<KeyValuePair<string, string>> yearOptions = await _dbContext.ProductOrders
                .Where(x => x.UserId == userId)
                .Select(x => new KeyValuePair<string, string>(x.Date.Year.ToString(), "year-" + x.Date.Year.ToString()))
                .Distinct()
                .ToListAsync();

            // Combine the two filters together
            filterOptions.AddRange(yearOptions.OrderByDescending(x => x.Key));

            return Result.Succeeded(filterOptions);
        }
    }
}