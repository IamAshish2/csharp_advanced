using interview_prep.Models.job;
using interview_prep.Repositories.Interfaces;
using interview_prep.Repositories.Repository;
using interview_prep.Services.Interfaces;

namespace interview_prep.Services.Service;

public class JobService (IJobRepository jobRepository) : IJobService
{
    private readonly IJobRepository _jobRepository = jobRepository;

    public async Task<bool> GetJobById(int jobId)
    {
        return await _jobRepository.DoesJobExist(jobId);
    }
}