namespace Website.Domain.Entities
{
    public sealed class NotificationGroup
    {
        public Guid Id { get; set; }
        public DateTime? ArchiveDate { get; set; }

        public ICollection<Notification> Notifications { get; private set; } = new HashSet<Notification>();

        public static NotificationGroup Create()
        {
            NotificationGroup notificationGroup = new()
            {
                Id = Guid.NewGuid()
            };

            return notificationGroup;
        }
    }
}