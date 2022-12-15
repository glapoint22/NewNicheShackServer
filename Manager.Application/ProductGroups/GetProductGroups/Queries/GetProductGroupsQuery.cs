using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.ProductGroups.GetProductGroups.Queries
{
    public sealed record GetProductGroupsQuery(string? ProductId) : IRequest<Result>;
}