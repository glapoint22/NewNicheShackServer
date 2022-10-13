using Website.Domain.Entities;
using Website.Domain.Interfaces;

namespace Website.Domain.Events
{
    public record UserChangedImageEvent(User User) : IUserUpdatedEvent;
}