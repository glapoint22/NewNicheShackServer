using MediatR;

namespace Manager.Domain.Events
{
    public sealed record UserAccountTerminatedEvent(string FirstName, string LastName, string Email) : INotification;
}
