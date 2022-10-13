using MediatR;
using Website.Domain.Entities;

namespace Website.Domain.Interfaces
{
    public interface IUserUpdatedEvent : INotification
    {
        User User { get; }
    }
}
