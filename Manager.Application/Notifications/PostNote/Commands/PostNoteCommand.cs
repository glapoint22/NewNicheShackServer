using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.PostNote.Commands
{
    public sealed record PostNoteCommand(Guid NotificationGroupId, Guid NotificationId, string Note) : IRequest<Result>;
}