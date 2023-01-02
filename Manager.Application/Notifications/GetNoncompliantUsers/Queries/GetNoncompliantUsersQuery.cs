using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.GetNoncompliantUsers.Queries
{
    public sealed record GetNoncompliantUsersQuery() : IRequest<Result>;
}