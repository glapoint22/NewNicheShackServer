using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.CreateNotification.Commands
{
    public sealed record CreateNotificationCommand(Guid UserId, int Type, string? UserName, string? UserImage, string? EmployeeNotes) : IRequest<Result>;
}