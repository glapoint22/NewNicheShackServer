using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.CheckDuplicateSubniche.Queries
{
    public sealed record CheckDuplicateSubnicheQuery(Guid ChildId, string ChildName) : IRequest<Result>;
}