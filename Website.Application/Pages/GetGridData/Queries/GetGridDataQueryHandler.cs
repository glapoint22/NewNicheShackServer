using MediatR;
using Shared.PageBuilder.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.Pages.GetGridData.Queries
{
    public sealed class GetGridDataQueryHandler : IRequestHandler<GetGridDataQuery, Result>
    {
        private readonly IPageService _pageService;

        public GetGridDataQueryHandler(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task<Result> Handle(GetGridDataQuery request, CancellationToken cancellationToken)
        {
            // Set the params
            PageParams pageParams = new(
                request.SearchTerm,
                request.NicheId,
                request.SubnicheId,
                request.SortBy,
                request.Filters,
                request.Page);

            var gridData = await _pageService.GetGridData(pageParams);

            return Result.Succeeded(gridData);
        }
    }
}