using MediatR;
using Website.Domain.Entities;

namespace Website.Domain.Events
{
    public record ActivateAccountSucceededEvent(User User) : INotification;
}