namespace Website.Domain.Events
{
    public sealed record UserChangedNameEvent : UserEvent
    {
        public UserChangedNameEvent(string UserId) : base(UserId) { }
    }
}