using AutoMapper;
using interview_prep.Models;
using interview_prep.Models.user;

namespace interview_prep.Dto.CloudinaryDTO;

[AutoMap(typeof(ProfilePhoto), ReverseMap = true)]
public class PictureReturnDTO
{
    public string Id { get; set; }
    public string UserId  { get; set; }
    public string Url { get; set; }
    public string Description { get; set; }
    public string PublicId { get; set; }
    public DateTime DateAdded { get; set; }
    public bool IsMain { get; set; }
}