using MediatR;
using Shared.PageBuilder.Classes;
using Website.Application.Common.Classes;

namespace Website.Application.Pages.GetGridData.Queries
{
    public sealed class GetGridDataQueryHandler : IRequestHandler<GetGridDataQuery, Result>
    {
        private readonly GridData _gridData;

        public GetGridDataQueryHandler(GridData gridData)
        {
            _gridData = gridData;
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

            GridData gridData = await _gridData.GetData(pageParams);

            return Result.Succeeded(gridData);
        }
    }
}