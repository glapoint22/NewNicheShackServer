namespace Manager.Domain.Entities
{
    public sealed class NotificationEmployeeNote
    {
        public Guid Id { get; set; }
        public Guid NotificationGroupId { get; set; }
        public Guid? NotificationId { get; set; }
        public string EmployeeId { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }

        public User User { get; set; } = null!;


        public static NotificationEmployeeNote Create(Guid notificationGroupId, Guid notificationId, string employeeId, string note)
        {
            return new NotificationEmployeeNote
            {
                Id = Guid.NewGuid(),
                NotificationGroupId = notificationGroupId,
                NotificationId = notificationId,
                EmployeeId = employeeId,
                Note = note,
                CreationDate= DateTime.UtcNow,
            };
        }
    }
}