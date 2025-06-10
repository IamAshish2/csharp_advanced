using interview_prep.Dto.CloudinaryDTO;
using interview_prep.Models.application;

namespace interview_prep.Repositories.Interfaces;

public interface IApplicationRepository
{
    Task<bool> SaveChangesAsync();
    Task<bool> AddNewJobApplication(Application application);
    Task<bool> AddCvDetails(Cv details);
    Task<bool> RemoveApplication(int applicationId);
    Task<bool> UpdateApplication(int applicationId, Application updatedApplication);
    Task<List<Application>> GetAllApplications(string userId);
}