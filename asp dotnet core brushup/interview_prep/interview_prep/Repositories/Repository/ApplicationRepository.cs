using interview_prep.Context;
using interview_prep.Models.application;
using interview_prep.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace interview_prep.Repositories.Repository;

public class ApplicationRepository (ApplicationDbContext context) : IApplicationRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddNewJobApplication(Application application)
    {
        await _context.Applications.AddAsync(application);
        return await SaveChangesAsync();
    }

    public async Task<bool> AddCvDetails(Cv details)
    {
        await _context.Cv.AddAsync(details);
        return await SaveChangesAsync();
    }

    public async Task<bool> RemoveApplication(int applicationId)
    {
        var application = await _context.Applications.FirstOrDefaultAsync(a => a.ApplicationId == applicationId);
        if (application == null) return false;
        
        _context.Applications.Remove(application);
        return await SaveChangesAsync();
    }


    public async Task<bool> UpdateApplication(int applicationId, Application updatedApplication)
    {
        if (applicationId != updatedApplication.ApplicationId)
        {
            return false;
        }
        
        var application = await _context.Applications.FirstOrDefaultAsync(a => a.ApplicationId == applicationId);
        
        if (application == null) return false;
        
        application.CvUrl = updatedApplication.CvUrl;
        application.CoverLetter = updatedApplication.CoverLetter;
        application.UpdatedAt = updatedApplication.UpdatedAt;
        return await SaveChangesAsync();

    }

    public async Task<List<Application>> GetAllApplications(string userId)
    {
        return await _context.Applications.Where(a => a.UserId == userId.Trim()).ToListAsync();
    }
}