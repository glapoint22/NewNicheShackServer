using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.RemoveUserImage.Commands
{
    public sealed record RemoveUserImageCommand(string UserId, string UserImage, Guid NotificationGroupId, Guid NotificationId) : IRequest<Result>;
}