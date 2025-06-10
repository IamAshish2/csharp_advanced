using System.Net;
using AutoMapper;
using interview_prep.Dto.CloudinaryDTO;
using interview_prep.Models;
using interview_prep.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace interview_prep.Controller;

[ApiController]
[Route("api/[controller]")]

public class UserProfileController : ControllerBase
{
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;

    public UserProfileController(ICloudinaryService cloudinaryService, IImageService imageService, IMapper mapper)
    {
        _cloudinaryService = cloudinaryService;
        _imageService = imageService;
        _mapper = mapper;
    }
    
    [HttpPost("upload-profile-image")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> UploadProfileImage(string userId, [FromForm] PictureUploadDTO uploadCredentials)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("","Invalid request received.");
            return BadRequest(ModelState);
        }

        try
        {
            var uploadResult = await _cloudinaryService.UploadToCloudinary(uploadCredentials);
            if (string.IsNullOrEmpty(uploadResult.Id))
            {
                ModelState.AddModelError("", "Image upload failed. Try again later.");
                return BadRequest(ModelState);
            }

            var profilePicture =  _mapper.Map<ProfilePhoto>(uploadResult);

            var imageUploadResult = await _imageService.UploadUserProfile(profilePicture);

            if (imageUploadResult.Errors.Count > 0)
            {
                foreach (var error in imageUploadResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return BadRequest(ModelState);
                }
            }

            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ProblemDetails()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "Internal Server Occured",
                Detail = "Please try again later. If the problem persists, contact support." + e.Message + e.StackTrace
            });
        }
    }
}