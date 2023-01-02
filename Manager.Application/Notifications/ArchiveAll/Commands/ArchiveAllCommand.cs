using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.ArchiveAll.Commands
{
    public sealed record ArchiveAllCommand(Guid NotificationGroupId) : IRequest<Result>;
}