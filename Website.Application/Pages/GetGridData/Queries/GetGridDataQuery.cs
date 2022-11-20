using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Pages.GetGridData.Queries
{
    public sealed record GetGridDataQuery(string? SearchTerm, string? NicheId, string? SubnicheId, string? Filters, int Page, string? SortBy) : IRequest<Result>;
}