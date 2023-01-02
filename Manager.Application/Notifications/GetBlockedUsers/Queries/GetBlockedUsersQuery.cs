using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.GetBlockedUsers.Queries
{
    public sealed record GetBlockedUsersQuery() : IRequest<Result>;
}