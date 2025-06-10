namespace interview_prep.Dto.CloudinaryDTO;


public class PictureUploadDTO
{
    public string? Url { get; set; }
    public string? UserId { get; set; }
    public IFormFile File { get; set; }
    public string? Description { get; set; }
    public string? PublicId { get; set; }
    public DateTime DateAdded { get; set; }

    public PictureUploadDTO()
    {
        DateAdded = DateTime.Now;
    }
}