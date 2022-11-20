using MediatR;

namespace Manager.Application.Notifications.GetNotificationCount.Queries
{
    public sealed record GetNotificationCountQuery() : IRequest<int>;
}