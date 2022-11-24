using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.GetProductNotification.Queries
{
    public sealed record GetProductNotificationQuery(Guid NotificationGroupId) : IRequest<Result>;
}