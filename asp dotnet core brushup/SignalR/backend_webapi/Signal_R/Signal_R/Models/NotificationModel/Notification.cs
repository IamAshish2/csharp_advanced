using Signal_R.enums;

namespace Signal_R.Models.NotificationModel
{
    public class Notification : BaseEntity
    {
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public NotificationType Type { get; set; } = NotificationType.Message;

        public bool IsRead { get; set; }
    }
}
