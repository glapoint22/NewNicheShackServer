using MediatR;

namespace Website.Domain.Events
{
    public record UserChangedNameEvent(string UserId) : INotification;
}