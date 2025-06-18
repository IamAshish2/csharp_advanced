namespace Signal_R.Models.MessageModel
{
    public class MessageEditHistory : BaseEntity
    {
        public long MessageId { get; set; }
        public Message Message { get; set; } = null!;

        public string EditedBy { get; set; } = null!;
        public User EditedByUser { get; set; } = null!;

        public string? PreviousContent { get; set; }
    }
}
