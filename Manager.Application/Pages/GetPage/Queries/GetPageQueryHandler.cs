using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.PageBuilder.Classes;

namespace Manager.Application.Pages.GetPage.Queries
{
    public sealed class GetPageQueryHandler : IRequestHandler<GetPageQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IPageService _pageService;

        public GetPageQueryHandler(IManagerDbContext dbContext, IPageService pageService)
        {
            _dbContext = dbContext;
            _pageService = pageService;
        }

        public async Task<Result> Handle(GetPageQuery request, CancellationToken cancellationToken)
        {
            Page page = await _dbContext.Pages
                .Where(x => x.Id == request.PageId)
                .SingleAsync();

            WebPage webPage = await _pageService.GetPage(page.Content);


            return Result.Succeeded(new
            {
                Id = request.PageId,
                page.Name,
                page.PageType,
                Content = webPage
            });
        }
    }
}