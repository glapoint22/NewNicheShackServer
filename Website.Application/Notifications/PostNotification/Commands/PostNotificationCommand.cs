using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Notifications.PostNotification.Commands
{
    public sealed record PostNotificationCommand : IRequest<Result>
    {
        public string? ProductId { get; init; }
        public int? ReviewId { get; init; }
        public int Type { get; init; }
        public string UserName { get; init; } = string.Empty;
        public string UserImage { get; init; } = string.Empty;
        public string Text { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string NonAccountName { get; init; } = string.Empty;
        public string NonAccountEmail { get; init; } = string.Empty;
    }
}