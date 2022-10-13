using Website.Domain.Entities;
using Website.Domain.Interfaces;

namespace Website.Domain.Events
{
    public record UserChangedNameEvent(User User) : IUserUpdatedEvent;
}