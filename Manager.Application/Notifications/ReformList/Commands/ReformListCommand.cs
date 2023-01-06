using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.ReformList.Commands
{
    public sealed record ReformListCommand(string ListId, int Option, string UserId, Guid NotificationGroupId, Guid NotificationId) : IRequest<Result>;
}