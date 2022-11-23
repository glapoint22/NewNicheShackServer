using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.Archive.Commands
{
    public sealed record ArchiveCommand(Guid NotificationGroupId, Guid NotificationId) : IRequest<Result>;
}