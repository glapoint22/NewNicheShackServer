using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.GetArchivedNotifications.Queries
{
    public sealed record GetArchivedNotificationsQuery() : IRequest<Result>;
}