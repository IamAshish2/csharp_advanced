using interview_prep.Dto.ApplicationDTO;
using interview_prep.Dto.CloudinaryDTO;
using interview_prep.Enums;
using interview_prep.Models.application;
using interview_prep.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace interview_prep.Controller.ApplicationController;

[ApiController]
[Route("api/[controller]")]

public class ApplicationController(IJobService jobService, ICloudinaryService cloudinaryService, IApplicationService applicationService) : ControllerBase
{
    private readonly IJobService _jobService = jobService;
    private readonly ICloudinaryService _cloudinaryService = cloudinaryService;
    private readonly IApplicationService _applicationService = applicationService;


    [HttpPost("apply-for-job/{jobId}",Name = "ApplyForJob")]
    public async Task<ActionResult> ApplyForJob(int jobId, [FromForm] JobApplicationDTO application)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("Form error","The application cannot be processed at the moment. Please try filing the form again.");
            return BadRequest(ModelState);
        }
        
        // validate the job id
        if (! await _jobService.GetJobById(jobId))
        {
            return BadRequest("Cannot apply to job. The job does not exist.");
        }

        var fileUpload = new FileUploadDTO()
        {
            File = application.Cv,
        };
        
        // upload the cv to cloudinary
        var uploadedCv = await _cloudinaryService.UploadFileToCloudinary(fileUpload);
        var mappedCv = new Cv()
        {
            UserId = application.UserId,
            CvUrl = uploadedCv.CvUrl,
            IsMain = uploadedCv.IsMain,
            DateAdded = uploadedCv.DateAdded,
            Description = uploadedCv.Description,
            PublicId = uploadedCv.PublicId
        };

        await _applicationService.AddCvDetails(mappedCv);
        

        // map the dto to application model
        var mappedApplication = new Application()
        {
            CvUrl = uploadedCv.CvUrl,
            CoverLetter = application.CoverLetter,
            UserId = application.UserId,
            AppliedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Status = ApplicationStatus.Pending,
            JobId = application.JobId
        };
        
        // add the application to the database
        if (! await _applicationService.AddApplicationForJob(mappedApplication) )
        {
            return BadRequest("The application could not be submitted at the moment. Please try again later.");
        }

        return NoContent();
        // return response

    }
}