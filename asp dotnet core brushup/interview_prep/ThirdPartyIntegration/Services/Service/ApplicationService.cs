using interview_prep.Context;
using interview_prep.Models.application;
using interview_prep.Repositories.Interfaces;
using interview_prep.Services.Interfaces;

namespace interview_prep.Services.Service;

public class ApplicationService (IApplicationRepository repository) : IApplicationService
{
    private readonly IApplicationRepository _repository = repository;

    public async Task<bool> AddApplicationForJob(Application application)
    {
        return await _repository.AddNewJobApplication(application);
    }

    public async Task<bool> AddCvDetails(Cv cv)
    {
        return await _repository.AddCvDetails(cv);
    }
}