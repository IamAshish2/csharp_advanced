using System.ComponentModel.DataAnnotations;

namespace Signal_R.Models.AttachmentModel
{
    public class AttachmentType : BaseEntity
    {
        [Required, MaxLength(100)]
        public string TypeName { get; set; } = null!;
    }
}
