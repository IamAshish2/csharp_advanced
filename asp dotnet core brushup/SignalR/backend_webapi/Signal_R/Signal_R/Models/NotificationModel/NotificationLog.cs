using Signal_R.enums;

namespace Signal_R.Models.NotificationModel
{
    public class NotificationLog : BaseEntity
    {
        public long NotificationId { get; set; }
        public Notification Notification { get; set; } = null!;

        public NotificationLogStatus Status { get; set; } = NotificationLogStatus.Sent;
    }
}
