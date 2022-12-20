using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.GetSubnicheParent.Queries
{
    public sealed record GetSubnicheParentQuery(Guid ChildId) : IRequest<Result>;
}