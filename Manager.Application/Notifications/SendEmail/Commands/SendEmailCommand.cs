using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.SendEmail.Commands
{
    public sealed record SendEmailCommand(Guid NotificationGroupId, Guid NotificationId, string Email) : IRequest<Result>;
}