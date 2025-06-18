using Signal_R.Models.AttachmentModel;
using System.ComponentModel.DataAnnotations;

namespace Signal_R.Models.AttachmentModel
{
    public class Attachment : BaseEntity
    {
        [Required, MaxLength(255)]
        public string FileName { get; set; } = null!;

        [Required, MaxLength(2048)]
        public string FileUrl { get; set; } = null!;

        public long FileSize { get; set; }

        [Required, MaxLength(100)]
        public string ContentType { get; set; } = null!;

        public long TypeId { get; set; }
        public AttachmentType Type { get; set; } = null!;

        public string UploadedBy { get; set; } = null!;
        public User UploadedByUser { get; set; } = null!;
    }
}
