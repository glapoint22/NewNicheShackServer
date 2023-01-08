using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.ReplaceUserName.Commands
{
    public sealed record ReplaceUserNameCommand(string UserId, string UserName, Guid NotificationGroupId, Guid NotificationId) : IRequest<Result>;
}