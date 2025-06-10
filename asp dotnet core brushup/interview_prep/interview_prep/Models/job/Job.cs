using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using interview_prep.Enums;
using interview_prep.Models.application;
using interview_prep.Models.user;

namespace interview_prep.Models.job;

public class Job
{
    [Key]
    public int JobId { get; set; }
    [MaxLength(255)]
    public required string JobName { get; set; }
    [MaxLength(255)]
    public string? Location { get; set; }
    [MaxLength(255)]
    public required JobType Type { get; set; } 
    [MaxLength(500)]
    public required string JobDescription { get; set; }
    public decimal Salary { get; set; }
    public DateTime PostedDate { get; set; }
    public DateTime Deadline { get; set; }
    
    [ForeignKey("UserId")]
    [MaxLength(255)]
    public required string EmployerId { get; set; }
    public User Employeer { get; set; }
    
    // one job has many applications
    public ICollection<Application> Applications = new List<Application>();
}