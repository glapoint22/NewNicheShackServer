namespace Website.Domain.Entities
{
    public sealed class NotificationGroup
    {
        public Guid Id { get; set; }
        public DateTime? ArchiveDate { get; set; }

        // Notifications
        private readonly List<Notification> _notifications = new();
        public IReadOnlyList<Notification> Notifications => _notifications.AsReadOnly();





        // ------------------------------------------------------------------------- Archive Notification ------------------------------------------------------------------------
        public void ArchiveNotification()
        {
            // Archive the group
            ArchiveDate = DateTime.UtcNow;

            // Archaive the notification
            if (_notifications.Count > 0)
            {
                _notifications[0].IsArchived = true;
            }
        }







        // ----------------------------------------------------------------------- Archive All Notifications ---------------------------------------------------------------------
        public void ArchiveAllNotifications()
        {
            // Archive the group
            ArchiveDate = DateTime.UtcNow;

            // Archaive the notification
            if (_notifications.Count > 0)
            {
                _notifications.ForEach(notification => notification.IsArchived = true);
            }
        }





        // ----------------------------------------------------------------------------- Archive Group ---------------------------------------------------------------------------
        public void ArchiveGroup()
        {
            // Archive the group
            ArchiveDate = DateTime.UtcNow;
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








        // ------------------------------------------------------------------------- Restore Notification ------------------------------------------------------------------------
        public void RestoreNotification(Guid notificationId)
        {
            if (_notifications.Count(x => x.IsArchived) == 1) ArchiveDate = null;

            Notification notification = _notifications
                .Where(x => x.Id == notificationId)
                .Single();

            notification.IsArchived = false;
        }







        // ----------------------------------------------------------------------- Restore All Notifications ---------------------------------------------------------------------
        public void RestoreAllNotifications()
        {
            // Restore the group
            ArchiveDate = null;

            // Archaive the notifications
            if (_notifications.Count > 0)
            {
                _notifications.ForEach(notification => notification.IsArchived = false);
            }
        }




        // ----------------------------------------------------------------------------- Restore Group ---------------------------------------------------------------------------
        public void RestoreGroup()
        {
            // Restore the group
            ArchiveDate = null;
        }
    }
}