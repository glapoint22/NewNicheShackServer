namespace Website.Domain.Entities
{
    public sealed class NotificationGroup
    {
        public Guid Id { get; set; }
        public DateTime? ArchiveDate { get; set; }

        // Notifications
        private readonly List<Notification> _notifications = new();
        public IReadOnlyList<Notification> Notifications => _notifications.AsReadOnly();





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







        // ------------------------------------------------------------------------------ Archive All ----------------------------------------------------------------------------
        public void ArchiveAll()
        {
            // Archive the group
            ArchiveDate = DateTime.UtcNow;

            // Archaive the notification
            if (_notifications.Count > 0)
            {
                _notifications.ForEach(notification => notification.IsArchived = true);
            }
        }






        // -------------------------------------------------------------------------------- Create ------------------------------------------------------------------------------
        public static NotificationGroup Create()
        {
            NotificationGroup notificationGroup = new()
            {
                Id = Guid.NewGuid()
            };

            return notificationGroup;
        }






        // -------------------------------------------------------------------------- Delete Notifications -----------------------------------------------------------------------
        public void DeleteNotifications(List<Guid> notificationIds)
        {
            _notifications.RemoveAll(x => notificationIds.Contains(x.Id));
        }








        // -------------------------------------------------------------------------------- Restore ------------------------------------------------------------------------------
        public void Restore(Guid notificationId)
        {
            if (_notifications.Count > 0)
            {
                if (_notifications.Count == 1) ArchiveDate = null;

                Notification notification = _notifications
                    .Where(x => x.Id == notificationId)
                    .Single();

                notification.IsArchived = false;
            }
        }







        // ------------------------------------------------------------------------------ Restore All ----------------------------------------------------------------------------
        public void RestoreAll()
        {
            // Restore the group
            ArchiveDate = null;

            // Archaive the notification
            if (_notifications.Count > 0)
            {
                _notifications.ForEach(notification => notification.IsArchived = false);
            }
        }
    }
}