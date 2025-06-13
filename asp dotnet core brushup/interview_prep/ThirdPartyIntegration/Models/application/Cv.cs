namespace interview_prep.Models.application;

public class Cv
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string CvUrl { get; set; }
    public string Description { get; set; }
    public string PublicId { get; set; }
    public DateTime DateAdded { get; set; }
    public bool IsMain { get; set; }
}