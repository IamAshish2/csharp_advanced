using interview_prep.Models.application;

namespace interview_prep.Services.Interfaces;

public interface IApplicationService
{
    Task<bool> AddApplicationForJob(Application application);
    Task<bool> AddCvDetails(Cv cv);
}