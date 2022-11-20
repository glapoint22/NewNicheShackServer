namespace Manager.Domain.Entities
{
    public sealed class NotificationEmployeeNote
    {
        public int Id { get; set; }
        public Guid NotificationGroupId { get; set; }
        public Guid? NotificationId { get; set; }
        public string EmployeeId { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }

        public User User { get; set; } = null!;
    }
}