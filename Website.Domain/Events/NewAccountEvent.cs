using MediatR;
using Website.Domain.Entities;

namespace Website.Domain.Events
{
    public record NewAccountEvent(User User) : INotification;
}