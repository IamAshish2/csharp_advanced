namespace interview_prep.Dto.ApplicationDTO;

public class JobApplicationDTO
{
    public required string UserId { get; set; }
    public int JobId { get; set; }
    public required IFormFile Cv { get; set; }
    public string? CoverLetter { get; set; }
}