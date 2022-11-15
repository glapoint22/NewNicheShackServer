namespace Website.Domain.Events
{
    public sealed record ListEditedEvent : ListEvent
    {
        public string PreviousName { get; init; }
        public string? PreviousDescription { get; init; }

        public ListEditedEvent(string UserId, string ListId, string PreviousName, string? PreviousDescription, string NewName, string? NewDescription) : base(UserId, ListId, NewName, NewDescription)
        {
            this.PreviousName = PreviousName;
            this.PreviousDescription = PreviousDescription;
        }
    }
}