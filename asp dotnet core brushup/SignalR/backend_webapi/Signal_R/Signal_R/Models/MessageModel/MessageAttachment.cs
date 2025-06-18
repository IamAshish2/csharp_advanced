using Signal_R.Models.AttachmentModel;
namespace Signal_R.Models.MessageModel
{
    public class MessageAttachment : BaseEntity
    {
        public long MessageId { get; set; }
        public Message Message { get; set; } = null!;

        public long AttachmentId { get; set; }
        public Attachment Attachment { get; set; } = null!;
    }
}
