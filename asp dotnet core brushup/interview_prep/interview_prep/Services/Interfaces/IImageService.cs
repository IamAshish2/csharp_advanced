using interview_prep.Dto.CloudinaryDTO;
using interview_prep.Models;
using interview_prep.Models.user;
using interview_prep.Responses;

namespace interview_prep.Services.Interfaces;

public interface IImageService
{
    Task<OperationalResult> UploadUserProfile(ProfilePhoto uploadCredentials);
}