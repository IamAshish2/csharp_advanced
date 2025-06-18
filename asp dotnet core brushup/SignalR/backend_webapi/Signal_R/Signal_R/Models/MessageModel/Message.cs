using Signal_R.Models.Converstaion;
using System.ComponentModel.DataAnnotations;

namespace Signal_R.Models.MessageModel
{
    public class Message : BaseEntity
    {
        public long ConversationId { get; set; }
        public Conversation Conversation { get; set; } = null!;

        public string SenderId { get; set; } = null!;
        public User Sender { get; set; } = null!;

        [Required]
        public string? Content { get; set; } 

        public bool IsEdited { get; set; }
    }

}
