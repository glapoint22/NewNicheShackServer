using MediatR;

namespace Website.Domain.Events
{
    public sealed record UserDeletedAccountOtpEvent(string UserId) : INotification;
}