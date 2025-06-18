using System.ComponentModel.DataAnnotations;

namespace Signal_R.Models.Contact
{
    public class ContactBlock : BaseEntity
    {
        public string UserId { get; set; } = null!;
        public User Blocker { get; set; } = null!;

        public string BlockedId { get; set; } = null!;
        public User Blocked { get; set; } = null!;

        [MaxLength(255)]
        public string? Reason { get; set; }
    }
}
