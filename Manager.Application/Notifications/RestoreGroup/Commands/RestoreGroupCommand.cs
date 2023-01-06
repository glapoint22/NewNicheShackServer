using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.RestoreGroup.Commands
{
    public sealed record RestoreGroupCommand(Guid NotificationGroupId) : IRequest<Result>;
}