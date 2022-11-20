using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.GetErrorNotification.Queries
{
    public sealed record GetErrorNotificationQuery(Guid NotificationGroupId) : IRequest<Result>;
}