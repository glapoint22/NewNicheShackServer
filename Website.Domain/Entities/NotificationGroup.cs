namespace Website.Domain.Entities
{
    public sealed class NotificationGroup
    {
        public Guid Id { get; set; }
        public DateTime? ArchiveDate { get; set; }

        // Notifications
        private readonly List<Notification> _notifications = new();
        public IReadOnlyList<Notification> Notifications => _notifications.AsReadOnly();




        // -------------------------------------------------------------------------------- Create ------------------------------------------------------------------------------
        public static NotificationGroup Create()
        {
            NotificationGroup notificationGroup = new()
            {
                Id = Guid.NewGuid()
            };

            return notificationGroup;
        }





        // -------------------------------------------------------------------------------- Archive ------------------------------------------------------------------------------
        public void Archive()
        {
            // Archive the group
            ArchiveDate = DateTime.UtcNow;

            // Archaive the notification
            if (_notifications.Count > 0)
            {
                _notifications[0].IsArchived = true;
            }
        }





        // -------------------------------------------------------------------------- Delete Notifications -----------------------------------------------------------------------
        public void DeleteNotifications(List<Guid> notificationIds)
        {
            _notifications.RemoveAll(x => notificationIds.Contains(x.Id));
        }
    }
}