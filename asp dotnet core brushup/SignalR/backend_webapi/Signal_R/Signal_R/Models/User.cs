using Microsoft.AspNetCore.Identity;
using Signal_R.Models.AttachmentModel;
using Signal_R.Models.Contact;
using Signal_R.Models.Converstaion;
using Signal_R.Models.MessageModel;
using Signal_R.Models.NotificationModel;

namespace Signal_R.Models
{
    public class User : IdentityUser
    {
        // Navigation properties
        public ICollection<Attachment> UploadedAttachments { get; set; } = new List<Attachment>();
        public ICollection<ContactBlock> BlocksInitiated { get; set; } = new List<ContactBlock>();
        public ICollection<ContactBlock> BlocksReceived { get; set; } = new List<ContactBlock>();
        public ICollection<ContactRelationship> Relationships { get; set; } = new List<ContactRelationship>();
        public ICollection<ContactRequest> RequestsSent { get; set; } = new List<ContactRequest>();
        public ICollection<ContactRequest> RequestsReceived { get; set; } = new List<ContactRequest>();
        public ICollection<Conversation> CreatedConversations { get; set; } = new List<Conversation>();
        public ICollection<ConversationMember> ConversationMemberships { get; set; } = new List<ConversationMember>();
        public ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public ICollection<MessageEditHistory> MessageEdits { get; set; } = new List<MessageEditHistory>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
