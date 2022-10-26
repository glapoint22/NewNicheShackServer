using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.Pages.GetPage.Queries
{
    public sealed class GetPageQueryHandler : IRequestHandler<GetPageQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public GetPageQueryHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetPageQuery request, CancellationToken cancellationToken)
        {
            string pageContent = await _dbContext.Pages
                .Where(x => x.Id == request.Id || x.PageType == request.PageType)
                .Select(x => x.Content)
                .SingleAsync();

            return Result.Succeeded();
        }
    }
}