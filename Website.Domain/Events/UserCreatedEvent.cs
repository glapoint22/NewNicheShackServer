namespace Website.Domain.Events
{
    public sealed record UserCreatedEvent : UserEvent
    {
        public UserCreatedEvent(string UserId) : base(UserId) { }
    }
}