using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using interview_prep.Configuration;
using interview_prep.Dto.CloudinaryDTO;
using interview_prep.Models;
using interview_prep.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace interview_prep.Services.Service;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> settings)
    {
        var cloudinarySettings = settings.Value;
        
        
        if (string.IsNullOrWhiteSpace(cloudinarySettings.CloudName) ||
            string.IsNullOrWhiteSpace(cloudinarySettings.ApiKey) ||
            string.IsNullOrWhiteSpace(cloudinarySettings.ApiSecretKey))
        {
            throw new ArgumentException("Invalid Cloudinary settings configuration");
        }

        var account = new Account(cloudinarySettings.CloudName, cloudinarySettings.ApiKey,
            cloudinarySettings.ApiSecretKey);
        // {
        //     Cloud = cloudinarySettings.CloudName,
        //     ApiKey = cloudinarySettings.ApiKey,
        //     ApiSecret = cloudinarySettings.ApiSecretKey,
        // };

        _cloudinary = new Cloudinary(account);
    }


    public async Task<PictureReturnDTO> UploadImageToCloudinary(PictureUploadDTO uploadCredentials)
    {
        var file = uploadCredentials.File;

        var uploadResult = new ImageUploadResult();

        if (file.Length > 0)
        {
            var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.Name, stream)
            };

            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }

        uploadCredentials.Url = uploadResult.Url.ToString();
        uploadCredentials.Description = "";
        uploadCredentials.PublicId = uploadResult.PublicId;

        var cloudinaryUploadResponse = new PictureReturnDTO()
        {
            Id = uploadResult.PublicId,
            UserId = uploadCredentials.UserId,
            Url = uploadResult.Url.ToString(),
            DateAdded = uploadCredentials.DateAdded,
            Description = "",
            PublicId = uploadResult.PublicId,
        };

        cloudinaryUploadResponse.IsMain = true;

        return cloudinaryUploadResponse;
    }

    public async Task<FileReturnDTO> UploadFileToCloudinary(FileUploadDTO uploadCredentials)
    {
        var file = uploadCredentials.File;
        var uploadResult = new RawUploadResult();

        if (file.Length > 0)
        {
            var stream = file.OpenReadStream();
            var uploadParams = new RawUploadParams()
            {
                File = new FileDescription(file.Name, stream)
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }

        uploadCredentials.PublicId = uploadResult.PublicId;
        uploadCredentials.Url = uploadResult.Url.ToString();
        uploadCredentials.Description = "";

        var uploadResponse = new FileReturnDTO()
        {
            Id = uploadCredentials.PublicId,
            UserId = uploadCredentials.UserId,
            CvUrl = uploadCredentials.Url,
            DateAdded = uploadCredentials.DateAdded,
            Description = "",
            PublicId = uploadResult.PublicId,
        };

        return uploadResponse;
    }
}