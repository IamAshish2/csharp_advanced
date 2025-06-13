using interview_prep.Models.job;

namespace interview_prep.Services.Interfaces;

public interface IJobService
{
    Task<bool> GetJobById(int jobId);
}