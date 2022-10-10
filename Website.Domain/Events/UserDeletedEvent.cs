using MediatR;
using Website.Domain.Entities;

namespace Website.Domain.Events
{
    public record UserDeletedEvent(User User) : INotification;
}