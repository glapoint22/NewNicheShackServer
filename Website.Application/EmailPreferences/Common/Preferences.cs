namespace Website.Application.EmailPreferences.Common
{
    public sealed record Preferences
    {
        public bool? NameUpdated { get; init; }
        public bool? EmailUpdated { get; init; }
        public bool? PasswordUpdated { get; init; }
        public bool? ProfileImageUpdated { get; init; }
        public bool? CollaboratorJoinedList { get; init; }
        public bool? UserJoinedList { get; init; }
        public bool? UserRemovedFromList { get; init; }
        public bool? CollaboratorRemovedFromList { get; init; }
        public bool? UserRemovedCollaborator { get; init; }
        public bool? CollaboratorAddedListItem { get; init; }
        public bool? UserAddedListItem { get; init; }
        public bool? CollaboratorRemovedListItem { get; init; }
        public bool? UserRemovedListItem { get; init; }
        public bool? CollaboratorMovedListItem { get; init; }
        public bool? UserMovedListItem { get; init; }
        public bool? CollaboratorUpdatedList { get; init; }
        public bool? UserUpdatedList { get; init; }
        public bool? CollaboratorDeletedList { get; init; }
        public bool? UserDeletedList { get; init; }
        public bool? ItemReviewed { get; init; }
    }
}