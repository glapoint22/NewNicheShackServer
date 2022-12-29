using MediatR;

namespace Website.Domain.Events
{
    public sealed record UserDeletedEvent(string FirstName, string LastName, string Email) : INotification;
}