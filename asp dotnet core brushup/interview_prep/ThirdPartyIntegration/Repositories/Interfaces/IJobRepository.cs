using interview_prep.Enums;
using interview_prep.Models.job;

namespace interview_prep.Repositories.Interfaces;

public interface IJobRepository
{
    Task<bool> SaveChangesAsync();
    Task<bool> AddNewJob(Job job);
    Task<bool> RemoveJob(int jobId);
    Task<bool> UpdateJob(int jobId, Job updatedJob);
    Task<bool> DoesJobExist(int jobId);
    Task<Job?> FilterJobById(int jobId);
    Task<List<Job>?> FilterJobByName(string name);
    Task<List<Job>?> FilterJobByType(JobType jobType);
    Task<List<Job>?> FilterJobByLocation(string location);
    Task<List<Job>?> FilterJobBySalaryRange(decimal lowerSalary, decimal upperSalary);
    Task<List<Job>?> FilterJobByDate(DateTime startDate, DateTime endDate);
}