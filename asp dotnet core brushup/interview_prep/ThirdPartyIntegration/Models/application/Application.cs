using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using interview_prep.Enums;
using interview_prep.Models.job;
using interview_prep.Models.user;

namespace interview_prep.Models.application;

public class Application
{
    [Key]
    public int ApplicationId { get; set; }
    [MaxLength(500)]
    public required string CvUrl { get; set; }
    [MaxLength(1000)]
    public string? CoverLetter { get; set; }

    [MaxLength(50)] public DateTime AppliedAt { get; set; } = DateTime.Now;
    [MaxLength(50)]
    public DateTime UpdatedAt { get; set; }

    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;// rejected, accepted, pending, reviewed
    
    [ForeignKey("UserId")]
    public string UserId { get; set; }
    public User User { get; set; }
    
    [ForeignKey("JobId")] 
    public int JobId { get; set; }
    public Job Job { get; set; }
}