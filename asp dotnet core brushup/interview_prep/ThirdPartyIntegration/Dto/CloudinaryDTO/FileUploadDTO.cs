namespace interview_prep.Dto.CloudinaryDTO;

public class FileUploadDTO
{
    public string UserId { get; set; }
    public string? Url { get; set; }
    public IFormFile File { get; set; }
    public string Description { get; set; }
    public string PublicId { get; set; }
    public DateTime DateAdded { get; set; }

    public FileUploadDTO()
    {
        DateAdded = DateTime.Now;
    }
}