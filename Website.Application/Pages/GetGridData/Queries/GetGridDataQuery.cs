using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Pages.GetGridData.Queries
{
    public sealed record GetGridDataQuery(string? SearchTerm, Guid? NicheId, Guid? SubnicheId, string? Filters, int Page, string? SortBy) : IRequest<Result>;
}