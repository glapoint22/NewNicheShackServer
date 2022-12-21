using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.GetFilterOptionParent.Queries
{
    public sealed record GetFilterOptionParentQuery(Guid ChildId) : IRequest<Result>;
}