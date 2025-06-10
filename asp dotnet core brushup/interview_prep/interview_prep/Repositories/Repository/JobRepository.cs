using interview_prep.Context;
using interview_prep.Enums;
using interview_prep.Models.job;
using interview_prep.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace interview_prep.Repositories.Repository;

public class JobRepository(ApplicationDbContext context) : IJobRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddNewJob(Job job)
    {
        await _context.Jobs.AddAsync(job);
        return await SaveChangesAsync();
    }

    public async Task<bool> RemoveJob(int jobId)
    {
        var job = await _context.Jobs.FirstOrDefaultAsync(j => j.JobId == jobId);

        if (job != null)
        {
             _context.Jobs.Remove(job);
             return await SaveChangesAsync();
        }

        return false;
    }

    public async Task<bool> UpdateJob(int jobId, Job updatedJob)
    {
        var job = await _context.Jobs.FirstOrDefaultAsync(j => j.JobId == jobId);
        if (job != null)
        {
            // Manually map properties from updatedJob to the tracked 'job' entity
            job.JobName = updatedJob.JobName;
            job.JobDescription = updatedJob.JobDescription;
            job.Salary = updatedJob.Salary;
            job.Location = updatedJob.Location;
            job.Type = updatedJob.Type;
            job.PostedDate = updatedJob.PostedDate;
            job.Deadline = updatedJob.Deadline;
        return await SaveChangesAsync();
        }

        return false;
    }

    public async Task<bool> DoesJobExist(int jobId)
    {
        return await _context.Jobs.AnyAsync(j => j.JobId == jobId);
    }

    public async Task<Job?> FilterJobById(int jobId)
    {
        return await _context.Jobs.FirstOrDefaultAsync(j => j.JobId == jobId);
    }

    public async Task<List<Job>?> FilterJobByName(string name)
    {
        // Escape '%' and '_' from user input if they are literal
        var escapedName = name.Replace("%", "\\%").Replace("_", "\\_");
        var patternToMatch = $"%{escapedName}%";
        return await _context.Jobs.Where(j => EF.Functions.Like(j.JobName.ToLower(), patternToMatch.ToLower())).ToListAsync();
    }

    public async Task<List<Job>?> FilterJobByType(JobType jobType)
    {
        return await _context.Jobs.Where(j => j.Type == jobType).ToListAsync();
    }

    public async Task<List<Job>?> FilterJobByLocation(string location)
    {
        var escapedLocation = location.Replace("%", "\\%").Replace("_", "\\_");
        var patternToMatch = $"%{escapedLocation}%"; // Add wildcards
        return await _context.Jobs.Where(j => EF.Functions.Like(j.Location.ToLower(), patternToMatch.ToLower(), "\\")).ToListAsync();
    }

    public async Task<List<Job>?> FilterJobBySalaryRange(decimal lowerSalary, decimal upperSalary)
    {
        if (lowerSalary > upperSalary)
        {
            return new List<Job>();
        }
        return await _context.Jobs
            .Where(j => j.Salary >= lowerSalary && j.Salary <= upperSalary)
            .ToListAsync();
    }

    public async Task<List<Job>?> FilterJobByDate(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            return new List<Job>();
        }
        return await _context.Jobs
            .Where(j => j.PostedDate >= startDate && j.Deadline < endDate.AddDays(1))
            .ToListAsync();
    }
}