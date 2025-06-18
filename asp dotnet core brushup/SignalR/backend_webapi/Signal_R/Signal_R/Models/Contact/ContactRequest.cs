using Signal_R.enums;

namespace Signal_R.Models.Contact
{
    public class ContactRequest : BaseEntity
    {
        public string RequesterId { get; set; } = null!;
        public User Requester { get; set; } = null!;

        public string RequesteeId { get; set; } = null!;
        public User Requestee { get; set; } = null!;

        public ContactRequestStatus Status { get; set; } = ContactRequestStatus.Pending;

        public DateTime? RespondedAt { get; set; }
    }
}
