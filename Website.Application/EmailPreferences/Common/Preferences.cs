namespace Website.Application.EmailPreferences.Common
{
    public record Preferences
    {
        public bool? NameChange { get; init; }
        public bool? EmailChange { get; init; }
        public bool? PasswordChange { get; init; }
        public bool? ProfileImageChange { get; init; }
        public bool? NewCollaborator { get; init; }
        public bool? RemovedCollaborator { get; init; }
        public bool? RemovedListItem { get; init; }
        public bool? MovedListItem { get; init; }
        public bool? AddedListItem { get; init; }
        public bool? EditedList { get; init; }
        public bool? DeletedList { get; init; }
        public bool? Review { get; init; }
    }
}