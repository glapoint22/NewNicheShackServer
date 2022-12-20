using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Niches.GetNicheChildren.Queries
{
    public sealed record GetNicheChildrenQuery(Guid ParentId) : IRequest<Result>;
}