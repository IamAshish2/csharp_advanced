using Signal_R.enums;

namespace Signal_R.Models.Converstaion
{
    public class ConversationMember : BaseEntity
    {
        public long ConversationId { get; set; }
        public Conversation Conversation { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public ConversationRole Role { get; set; } = ConversationRole.Member;
    }
}
