namespace Signal_R.Models
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public string Uuid { get; set; } = Guid.NewGuid().ToString();
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateUpdated { get; set; }
    }
}
