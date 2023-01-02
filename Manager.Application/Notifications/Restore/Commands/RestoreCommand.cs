using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.Restore.Commands
{
    public sealed record RestoreCommand(Guid NotificationGroupId, Guid NotificationId) : IRequest<Result>;
}