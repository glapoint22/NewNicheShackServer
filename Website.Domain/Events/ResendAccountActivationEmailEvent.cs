using MediatR;

namespace Website.Domain.Events
{
    public sealed record ResendAccountActivationEmailEvent(string UserId) : INotification;
}
