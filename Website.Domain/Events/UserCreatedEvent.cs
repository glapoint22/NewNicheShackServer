using MediatR;
using Website.Domain.Entities;

namespace Website.Domain.Events
{
    public record UserCreatedEvent(User User) : INotification;
}
