using MediatR;

namespace Website.Domain.Events
{
    public sealed record UserChangedEmailOtpEvent(string UserId, string Email) : INotification;
}
