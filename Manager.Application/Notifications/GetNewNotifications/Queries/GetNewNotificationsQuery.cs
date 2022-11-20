using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.GetNewNotifications.Queries
{
    public sealed record GetNewNotificationsQuery() : IRequest<Result>;
}