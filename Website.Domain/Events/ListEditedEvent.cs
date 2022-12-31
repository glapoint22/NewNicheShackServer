namespace Website.Domain.Events
{
    public sealed record ListEditedEvent : ListEvent
    {
        public string PreviousName { get; init; }
        public string? PreviousDescription { get; init; }
        public string NewName { get; init; }
        public string? NewDescription { get; init; }

        public ListEditedEvent(string userId, string listId, string previousName, string? previousDescription, string newName, string? newDescription) : base(userId, listId, newName, newDescription)
        {
            PreviousName = previousName;
            PreviousDescription = previousDescription;
            NewName = newName;
            NewDescription = newDescription;
        }
    }
}