using interview_prep.Context;
using interview_prep.Dto.CloudinaryDTO;
using interview_prep.Models;
using interview_prep.Models.user;
using interview_prep.Repositories.Interfaces;
using interview_prep.Responses;
using interview_prep.Services.Interfaces;

namespace interview_prep.Services.Service;

public class ImageService : IImageService
{
    private readonly IUserRepository _userRepository;

    public ImageService (IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<OperationalResult> UploadUserProfile(ProfilePhoto uploadCredentials)
    {
        if (string.IsNullOrEmpty(uploadCredentials.UserId))
        {
            return OperationalResult.FailureResult(new [] {"Please login and try again later."});
        }
        
        var uploadImage = await _userRepository.UploadUserImage(uploadCredentials);

        if (!uploadImage)
        {
            return OperationalResult.FailureResult(new[] { "Error uploading image. The server is busy. Please try again later." });
            
        }


        return OperationalResult.SuccessResult(new [] {"Successfully uploaded image. Refresh to continue"});
    }
}