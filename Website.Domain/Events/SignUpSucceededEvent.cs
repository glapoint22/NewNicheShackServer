using MediatR;
using Website.Domain.Entities;

namespace Website.Domain.Events
{
    public record SignUpSucceededEvent(User User) : INotification;

}