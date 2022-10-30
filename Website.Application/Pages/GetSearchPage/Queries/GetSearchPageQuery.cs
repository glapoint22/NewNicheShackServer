using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Pages.GetSearchPage.Queries
{
    public sealed record GetSearchPageQuery(string SearchTerm, int? NicheId, int? SubnicheId, string? Filters, int Page, string? SortBy) : IRequest<Result>;
}