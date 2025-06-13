using interview_prep.Dto.CloudinaryDTO;

namespace interview_prep.Services.Interfaces;

public interface ICloudinaryService
{
    Task<PictureReturnDTO> UploadImageToCloudinary(PictureUploadDTO uploadCredentials);
    Task<FileReturnDTO> UploadFileToCloudinary(FileUploadDTO uploadCredentials);
}