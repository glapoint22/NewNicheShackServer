using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.RestoreAll.Commands
{
    public sealed record RestoreAllCommand(Guid NotificationGroupId) : IRequest<Result>;
}