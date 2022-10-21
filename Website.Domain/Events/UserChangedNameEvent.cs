using MediatR;

namespace Website.Domain.Events
{
    public sealed record UserChangedNameEvent(string UserId) : INotification;
}