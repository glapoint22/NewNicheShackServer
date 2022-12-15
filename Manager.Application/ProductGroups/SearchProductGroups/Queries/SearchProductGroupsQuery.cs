using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.ProductGroups.SearchProductGroups.Queries
{
    public sealed record SearchProductGroupsQuery(string? ProductId, string SearchTerm) : IRequest<Result>;
}