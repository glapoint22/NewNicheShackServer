using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.ArchiveGroup.Commands
{
    public sealed record ArchiveGroupCommand(Guid NotificationGroupId) : IRequest<Result>;
}