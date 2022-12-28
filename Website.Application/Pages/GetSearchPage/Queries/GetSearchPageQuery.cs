using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Pages.GetSearchPage.Queries
{
    public sealed record GetSearchPageQuery(string SearchTerm, Guid? NicheId, Guid? SubnicheId, string? Filters, int Page, string? SortBy) : IRequest<Result>;
}