using System.ComponentModel.DataAnnotations;

namespace Signal_R.Models.Converstaion
{
    public class Conversation : BaseEntity
    {
        [MaxLength(255)]
        public string? Title { get; set; }

        public bool IsGroup { get; set; }

        public string CreatedBy { get; set; } = null!;
        public User Creator { get; set; } = null!;
    }
}
