using Website.Domain.Entities;
using Website.Domain.Interfaces;

namespace Website.Domain.Events
{
    public record UserChangedEmailEvent(User User) : IUserUpdatedEvent;
}