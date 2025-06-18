namespace Signal_R.Models.Contact
{
    public class ContactRelationship : BaseEntity
    {
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public string ContactId { get; set; } = null!;
        public User Contact { get; set; } = null!;
    }
}
