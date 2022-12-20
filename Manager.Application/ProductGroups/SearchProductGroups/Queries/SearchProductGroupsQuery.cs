using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.ProductGroups.SearchProductGroups.Queries
{
    public sealed record SearchProductGroupsQuery(Guid? ProductId, string SearchTerm) : IRequest<Result>;
}